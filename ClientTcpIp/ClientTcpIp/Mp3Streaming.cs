using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using NAudio.Wave;

namespace ClientTcpIp
{
    public class Mp3Streaming : IDisposable
    {
        enum StreamingPlaybackState
        {
            Stopped,
            Playing,
            Buffering,
            Paused
        }
        public void Dispose() 
        {
            MP3StreamingPanel_Disposing(this,new StoppedEventArgs());
        }

        public Mp3Streaming(NetworkStream ns)
        {
            //volumeSlider1.VolumeChanged += OnVolumeSliderChanged;            
            
            this.networkStream = ns;
            timer1 = new System.Timers.Timer(250);            
            timer1.Elapsed += timer1_Tick;
            timer1.Enabled = true;
            timer1.Start();
        }

        //void OnVolumeSliderChanged(object sender, EventArgs e)
        //{
        //    if (volumeProvider != null)
        //    {
        //        volumeProvider.Volume = volumeSlider1.Volume;
        //    }
        //}

        private NetworkStream networkStream;
        private System.Timers.Timer timer1;
        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private volatile StreamingPlaybackState playbackState;
        private volatile bool fullyDownloaded;        
        private VolumeWaveProvider16 volumeProvider;

        delegate void ShowErrorDelegate(string message);
        
        private void StreamMp3(object state)
        {
            fullyDownloaded = false;
            var buffer = new byte[16384 * 4]; // needs to be big enough to hold a decompressed frame

            IMp3FrameDecompressor decompressor = null;
            try
            {
                using (var responseStream = networkStream)
                {
                    var readFullyStream = new ReadFullyStream(responseStream);
                    do
                    {
                        if (IsBufferNearlyFull)
                        {
                            Console.WriteLine("Buffer getting full, taking a break");
                            Thread.Sleep(500);
                        }
                        else
                        {
                            Mp3Frame frame;
                            try
                            {
                                if (playbackState == StreamingPlaybackState.Stopped)
                                    break;
                                
                                frame = Mp3Frame.LoadFromStream(readFullyStream);                                
                            }
                            catch (EndOfStreamException)
                            {
                                fullyDownloaded = true;
                                // reached the end of the MP3 file / stream
                                break;
                            }
                            catch (WebException)
                            {
                                // probably we have aborted download from the GUI thread
                                break;
                            }
                            if (frame == null) break;
                            if (decompressor == null)
                            {
                                // don't think these details matter too much - just help ACM select the right codec
                                // however, the buffered provider doesn't know what sample rate it is working at
                                // until we have a frame
                                decompressor = CreateFrameDecompressor(frame);
                                bufferedWaveProvider = new BufferedWaveProvider(decompressor.OutputFormat);
                                bufferedWaveProvider.BufferDuration =
                                    TimeSpan.FromSeconds(20); // allow us to get well ahead of ourselves
                                //this.bufferedWaveProvider.BufferedDuration = 250;
                            }
                            int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                            //Debug.WriteLine(String.Format("Decompressed a frame {0}", decompressed));
                            bufferedWaveProvider.AddSamples(buffer, 0, decompressed);                            
                        }

                    } while (playbackState != StreamingPlaybackState.Stopped);
                    Console.WriteLine("Exiting");
                    // was doing this in a finally block, but for some reason
                    // we are hanging on response stream .Dispose so never get there
                    decompressor?.Dispose();
                    StopPlayback();
                }
            }
            finally
            {
                if (decompressor != null)
                {
                    decompressor.Dispose();
                }
            }
        }
        private static IMp3FrameDecompressor CreateFrameDecompressor(Mp3Frame frame)
        {
            WaveFormat waveFormat = new Mp3WaveFormat(frame.SampleRate, frame.ChannelMode == ChannelMode.Mono ? 1 : 2,
                frame.FrameLength, frame.BitRate);
            return new AcmMp3FrameDecompressor(waveFormat);
        }

        private bool IsBufferNearlyFull
        {
            get
            {
                return bufferedWaveProvider != null &&
                       bufferedWaveProvider.BufferLength - bufferedWaveProvider.BufferedBytes
                       < bufferedWaveProvider.WaveFormat.AverageBytesPerSecond / 4;
            }
        }

        public void Riproduci() 
        {
            buttonPlay_Click();
        }

        private void buttonPlay_Click()
        {
            if (playbackState == StreamingPlaybackState.Stopped)
            {                
                playbackState = StreamingPlaybackState.Buffering;
                bufferedWaveProvider = null;
                StreamMp3(this);
                ThreadPool.QueueUserWorkItem(StreamMp3);                
            }
            else if (playbackState == StreamingPlaybackState.Paused)
            {
                playbackState = StreamingPlaybackState.Buffering;
            }
        }

        private void StopPlayback()
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (!fullyDownloaded)
                {
                    networkStream.Close();
                }

                playbackState = StreamingPlaybackState.Stopped;
                if (waveOut != null)
                {
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }
                timer1.Enabled = false;
                // n.b. streaming thread may not yet have exited
                Thread.Sleep(500);
                ShowBufferState(0);
            }
        }

        private void ShowBufferState(double totalSeconds)
        {
            Console.WriteLine(String.Format("{0:0.0}s", totalSeconds));
            Console.WriteLine((int)(totalSeconds * 1000));
        }

        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            if (playbackState != StreamingPlaybackState.Stopped)
            {
                if (waveOut == null && bufferedWaveProvider != null)
                {
                    Console.WriteLine("Creating WaveOut Device");
                    waveOut = CreateWaveOut();
                    waveOut.PlaybackStopped += OnPlaybackStopped;
                    volumeProvider = new VolumeWaveProvider16(bufferedWaveProvider);
                    //volumeProvider.Volume = volumeSlider1.Volume;
                    waveOut.Init(volumeProvider);
                    //progressBarBuffer.Maximum = (int)bufferedWaveProvider.BufferDuration.TotalMilliseconds;
                }
                else if (bufferedWaveProvider != null)
                {
                    var bufferedSeconds = bufferedWaveProvider.BufferedDuration.TotalSeconds;
                    ShowBufferState(bufferedSeconds);
                    // make it stutter less if we buffer up a decent amount before playing
                    if (bufferedSeconds < 0.5 && playbackState == StreamingPlaybackState.Playing && !fullyDownloaded)
                    {
                        Pause();
                    }
                    else if (bufferedSeconds > 4 && playbackState == StreamingPlaybackState.Buffering)
                    {
                        Play();
                    }
                    else if (fullyDownloaded && bufferedSeconds < 1)
                    {
                        Console.WriteLine("Reached end of stream");
                        StopPlayback();
                    }
                }
                
            }
        }

        private void Play()
        {
            try
            {
                waveOut.Play();
                Console.WriteLine(String.Format("Started playing, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                playbackState = StreamingPlaybackState.Playing;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void Pause()
        {
            playbackState = StreamingPlaybackState.Buffering;
            waveOut.Pause();
            if (bufferedWaveProvider.BufferedDuration.TotalSeconds < 1)
                fullyDownloaded = true;

            if(fullyDownloaded)
                this.MP3StreamingPanel_Disposing(this,new StoppedEventArgs());
            else
                Console.WriteLine(String.Format("Paused to buffer, waveOut.PlaybackState={0}", waveOut.PlaybackState));
        }

        private IWavePlayer CreateWaveOut()
        {
            return new WaveOut();
        }

        private void MP3StreamingPanel_Disposing(object sender, StoppedEventArgs e)
        {
            StopPlayback();
        }

        public void Pausa()
        {
            buttonPause_Click();
        }

        private void buttonPause_Click()
        {
            if (playbackState == StreamingPlaybackState.Playing || playbackState == StreamingPlaybackState.Buffering)
            {
                waveOut.Pause();
                Console.WriteLine(String.Format("User requested Pause, waveOut.PlaybackState={0}", waveOut.PlaybackState));
                playbackState = StreamingPlaybackState.Paused;
            }
        }

        public void Finisci() 
        {
            buttonStop_Click();
        }

        private void buttonStop_Click()
        {
            StopPlayback();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            Console.WriteLine("Playback Stopped");
            if (e.Exception != null)
            {
                Console.WriteLine(String.Format("Playback Error {0}", e.Exception.Message));
            }
        }
    }    
}