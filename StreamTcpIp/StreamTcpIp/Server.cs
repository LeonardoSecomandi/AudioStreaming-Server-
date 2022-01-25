using NAudio.Wave;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    TcpListener server = null;
    public Server(string ip, int port)
    {
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, port);
        server.Start();
        StartListener();
    }

    public void StartListener()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");                

                Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                t.Start(client);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
            server.Stop();
        }
    }
    
    public static void SendFile(NetworkStream stream, string fileName)
    {
        if (File.Exists(fileName))
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);            
            fileStream.CopyToAsync(stream);
        }
        else
            Console.WriteLine("File non esiste");
        //stream.Flush();
        //stream.Close();
    }

    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();
        string imei = String.Empty;

        string data = null;
        Byte[] bytes = new Byte[32768];
        int i;
        try
        {
            //while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            //{                
            string hex = BitConverter.ToString(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);                               
            //}
            SendFile(stream, "Example.mp3");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            client.Close();
        }

    }
}