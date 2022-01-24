using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Threading;
using ClientTcpIp;

namespace ClientTcpIp
{
    class Program
    {    
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect("127.0.0.1", "ciao");
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

                
                var suona = new Mp3Streaming(stream);                
                suona.Riproduci();

                Thread.Sleep(2000);
            
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