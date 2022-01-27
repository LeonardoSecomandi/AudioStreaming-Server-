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
            bool download = false   ;
            string nomeCanzone = "Example1.mp3";

            if (download) nomeCanzone = "%d" + nomeCanzone;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", nomeCanzone);
            }).Start();

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

                if (!message.Contains("%d"))
                {
                    var suona = new Mp3Streaming(stream);
                    suona.Riproduci();
                }
                else
                {
                    using (Stream file = File.Create("fileAudio.mp3"))
                    {
                        //while (stream.Read()) 
                        //{
                        //    stream.CopyTo(file);
                        //}
                        
                    }
                }       

                Thread.Sleep(20);                

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
    }
}