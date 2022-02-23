using NAudio.Wave;
using System;
using System.Net.Sockets;
using System.Threading;
using ClientTcpIp;
using System.Text;
using System.IO;
using ClientTcpIp.Models;
using System.Linq;

namespace ClientTcpIp
{
    class Program
    {
        public static string stato;

        static void Main(string[] args)
        {
            CanzoniService service = new CanzoniService();
            var eleCanzoni = service.GetCanzoni().Result;

            bool continua = true;
            bool manda;
            bool download;
            string nomeCanzone;

            while (continua)
            {
                string risposta = "";
                do
                {
                    Console.Write("Continuare? [y/n]: ");
                    risposta = Console.ReadLine();
                } while (risposta != "y" && risposta != "n");
                continua = risposta == "y" ? true : false;
                if (continua)
                {
                    int i = 0;
                    foreach (var canzone in eleCanzoni)
                    {
                        Console.WriteLine(i + 1 + ". " + canzone.SongTitle);
                        i++;
                    }
                    bool b1 = false;
                    bool b2 = false;

                    do
                    {
                        Console.Write("Che canzone vuoi?[numero canzone]: ");
                        risposta = Console.ReadLine();
                        b1 = !risposta.All(char.IsDigit);
                        try
                        {
                            b2 = (eleCanzoni.Count() < Int32.Parse(risposta));
                        }
                        catch { }
                    } while (b1 || b2);
                    nomeCanzone = eleCanzoni.ToArray()[int.Parse(risposta) - 1].SongTitle;
                    string titolo = nomeCanzone;
                    nomeCanzone = nomeCanzone.Contains(".mp3") ? nomeCanzone : nomeCanzone + ".mp3";
                    do
                    {
                        Console.Write("Mandare al server? [y/n]: ");
                        risposta = Console.ReadLine();
                    } while (risposta != "y" && risposta != "n");
                    manda = risposta == "y" ? true : false;
                    dynamic bytes = default(dynamic);
                    if (manda) bytes = File.ReadAllBytes(nomeCanzone).ToString();

                    if (manda)
                    {
                        string canzone = "";
                        foreach (byte b in bytes)
                            canzone += b.ToString() + ",";
                        canzone = canzone.Remove(canzone.Length - 1);

                        nomeCanzone = "%u" + canzone + "%" + nomeCanzone;
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

                    var t = new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Connect("127.0.0.1", nomeCanzone, titolo);
                    });
                    t.Start();
                    t.Join();                    
                }
            }
            Console.ReadLine();            
        }

        static void Connect(String server, String message, string titolo)
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
                        var mp3Stream = new Mp3Streaming(stream);
                        new Thread(() => mp3Stream.Riproduci()).Start(); // avvia
                        stato = "play";
                        Console.WriteLine("I comandi sono: stop, play, pause");
                        bool f = true;
                        if (mp3Stream != null || stato != "stop")
                        {
                            if (f) { Console.WriteLine($"Il brano in riproduzione è {titolo}"); f = false; }
                            switch (Console.ReadLine())
                            {
                                case "stop":
                                    if (stato != "stop")
                                        new Thread(() =>
                                        {
                                            stato = "stop";
                                            mp3Stream.Finisci();
                                        }).Start();
                                    else
                                        Console.WriteLine("Già stoppato");
                                    break;
                                case "play":
                                    if (stato != "play")
                                        new Thread(() =>
                                        {
                                            stato = "play";
                                            mp3Stream.Riproduci();
                                        }).Start();
                                    else
                                        Console.WriteLine("Sta già suonando");
                                    break;
                                case "pause":
                                    if (stato != "pause")
                                        new Thread(() =>
                                        {
                                            stato = "pause";
                                            mp3Stream.Pausa();
                                        }).Start();
                                    else
                                        Console.WriteLine("Già in pausa");
                                    break;
                                default:
                                    Console.WriteLine("Comando non valido");
                                    break;
                            }
                        }
                        Console.WriteLine("Finita riproduzione");
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