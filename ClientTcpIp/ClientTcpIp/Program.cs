using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Threading;
using ClientTcpIp;
using System.Text;
using System.IO;

namespace ClientTcpIp
{
    class Program
    {        
        
        static void Main(string[] args)
        {
            bool manda;
            bool download;
            string nomeCanzone;

            Console.Write("Che canzone vuoi?: ");
            nomeCanzone = Console.ReadLine();
            nomeCanzone = nomeCanzone.Contains(".mp3") ? nomeCanzone : nomeCanzone + ".mp3";

            string risposta;
            do
            {
                Console.Write("Mandare un file? [y/n]: ");
                risposta = Console.ReadLine();
            } while (risposta != "y" && risposta != "n");
            manda = risposta == "y" ? true : false;
            var bytes = File.ReadAllBytes(nomeCanzone).ToString();

            if (manda) 
            {
                string canzone="";
                foreach (byte b in bytes)
                    canzone += b.ToString() + ",";
                canzone = canzone.Remove(canzone.Length - 1);

                nomeCanzone = "%u" + canzone +"%"+nomeCanzone;
            }                
            else
            {
                do
                {
                    Console.Write("Vuoi riprodurla in tempo reale? [y/n]: ");
                    risposta = Console.ReadLine();
                } while (risposta != "y" && risposta != "n");
                download = risposta == "y" ? false : true;
                if (download) nomeCanzone = "%d" + nomeCanzone;
            }

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", nomeCanzone);
            }).Start();

            Console.WriteLine("hai finito");
            Console.ReadLine();
        }


        static void Connect(String server, String message)
        {
            try
            {
                Int32 port = 13000;

                TcpClient client = new TcpClient(server, port);

                NetworkStream stream = client.GetStream();
                stream.Write(Encoding.ASCII.GetBytes(message));

                if (message.StartsWith("%u"))
                {
                    var buffer = new byte[1024];                    
                    Console.WriteLine("Audio inviato");
                    var bytes = new byte[1024];
                    stream.Read(bytes);
                    string data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    Console.WriteLine("Messaggio ricevuto");

                }
                else 
                {
                    if (!message.StartsWith("%d"))
                    {
                        var suona = new Mp3Streaming(stream);
                        new Thread(() => suona.Riproduci()).Start(); // avvia
                        while (suona != null)
                        {
                            Thread.Sleep(3000);
                            //new Thread(() => suona.Pausa()).Start(); // pausa
                            //Thread.Sleep(2000);
                            //new Thread(() => suona.Riproduci()).Start();
                            //Thread.Sleep(5000);
                            new Thread(() => suona.Finisci()).Start(); // ferma
                        }
                        Console.WriteLine("finito di suonare");
                    }
                    else
                    {
                        using (var fileIO = File.Create(message.Replace("%d", " ")))
                        {
                            var buffer = new byte[1024 * 8];
                            int count;

                            while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fileIO.Write(buffer, 0, count);
                            }
                        }
                        Console.WriteLine("Scaricazione completata");
                    }
                }                 
                
                Thread.Sleep(20);
                
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }            
        }
    }
}