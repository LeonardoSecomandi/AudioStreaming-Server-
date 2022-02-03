using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    
    public static void SendFile(NetworkStream stream, string fileName, bool download)
    {
        if (File.Exists(fileName))
        {
            if (download)
            {
                using (var fileIO = File.OpenRead(fileName))                
                {
                    var buffer = new byte[1024 * 8];
                    int count;
                    while ((count = fileIO.Read(buffer, 0, buffer.Length)) > 0)
                        stream.Write(buffer, 0, count);
                }
            }
            else
            {
                FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileStream.CopyToAsync(stream);
            }
        }
        else
            Console.WriteLine("File non esiste");        
    }
    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();
        
        string data = null;
        Byte[] bytes = new Byte[1028];
        try
        {
            stream.Read(bytes);
            string hex = BitConverter.ToString(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
            bool manda = data.Contains("%d");
            data = manda ? data.Split(".mp3")[0].Replace("%d", " ") + ".mp3" : data.Split(".mp3")[0] + ".mp3";
            Console.WriteLine("Mandando file");
            SendFile(stream, data.Trim(), manda);
            Console.WriteLine("Finito di mandare file");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e.ToString());
            client.Close();
        }
    }
}