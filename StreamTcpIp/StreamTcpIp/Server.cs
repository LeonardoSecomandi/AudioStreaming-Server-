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
    
    public static void SendFile(TcpClient client, string fileName, bool download)
    {
        NetworkStream stream = client.GetStream();
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
                stream.Close();
                client.Close();
            }
            else
            {
                FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileStream.CopyTo(stream);
            }
        }
        else
            Console.WriteLine("File non esiste");        
    }
    public void HandleDeivce(Object obj)
    {
        TcpClient client = (TcpClient)obj;
        var stream = client.GetStream();
        
        string data;
        Byte[] bytes = new Byte[1028];
        try
        {
            stream.Flush();
            
            stream.Read(bytes);            
            data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);
            bool manda = data.StartsWith("%d");
            bool carica = data.StartsWith("%u");
            if (!carica)
            {
                data = manda ? data.Split(".mp3")[0].Replace("%d", " ") + ".mp3" : data.Split(".mp3")[0] + ".mp3";
                Console.WriteLine("Mandando file");
                SendFile(client, data.Trim(), manda);
                Console.WriteLine("Finito di mandare file");
                if (manda)
                {
                    stream.Close();
                    client.Close();
                }
            }
            else 
            {
                data = data.Replace("%u", "");
                string nomeCanzone = data.Split("%")[1].Replace("\0","");
                data= data.Split("%")[0];
                byte[] array = new byte[1024];
                array = Encoding.ASCII.GetBytes("Ricevuto file");
                
                stream.Write(array,0,array.Length);
                array = new byte[1024];
                int i = 0;
                foreach (var a in data.Split(",")) 
                {
                    array[i] = Convert.ToByte(a);                    
                    i++;
                }
                File.WriteAllBytes(nomeCanzone, array);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.ToString());
            client.Close();
        }
    }
}