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
            bool download;
            string nomeCanzone;

            Console.Write("Che canzone vuoi?: ");
            nomeCanzone = Console.ReadLine();
            nomeCanzone = nomeCanzone.Contains(".mp3") ? nomeCanzone : nomeCanzone + ".mp3";

            string risposta;
            do
            {
                Console.Write("Vuoi scaricarla? [y/n]: ");
                risposta = Console.ReadLine();
            } while (risposta != "y" && risposta != "n");

            download = risposta == "y" ? true : false;

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
                    using (var fileIO = File.Create(message.Replace("%d"," ")))
                    {
                        var buffer = new byte[1024 * 8];
                        int count;
                        //bool continua = true;
                        while ((count = stream.Read(buffer, 0, buffer.Length)) > 0/* && continua*/) 
                        {
                            fileIO.Write(buffer, 0, count);
                            //foreach (var a in buffer)
                            //    if (a != 0)
                            //        continua = true;
                        }                            
                    }
                    Console.WriteLine("Scaricazione completata");                    
                }
                Thread.Sleep(20);
                
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.Read();
        }
    }
}