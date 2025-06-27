using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
namespace np_async_sockets
{
    internal class Program
    {
        static List<string> quotes = new List<string>
        {
            "The only way to do great work is to love what you do.",
            "The only limit to our realization of tomorrow will be our doubts of today.",
            "I may disagree with what you say, but I will defend to death your right to say it.",
            "People do what they hate for money and use the money to do what they love."
        };

        static void HandleClient(Socket client)
        {
            string clientInfo = client.RemoteEndPoint?.ToString() ?? "unknown";

            Console.WriteLine(DateTime.Now + " " + clientInfo + " connected");

            Random rnd = new Random();
            string quote = quotes[rnd.Next(quotes.Count)];

            Console.WriteLine(DateTime.Now + " Sent quote to " + clientInfo + ": " + quote);

            byte[] data = Encoding.UTF8.GetBytes(quote);
            client.Send(data);

            Console.WriteLine(DateTime.Now + " " + clientInfo + " disconnected\n");
            client.Close();
        }

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.1.3");
            int port = 5555;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            server.Bind(endPoint);
            server.Listen(10);

            Console.WriteLine("Server started on " + ip + ":" + port);

            while (true)
            {
                Socket client = server.Accept();
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }
    }
}
