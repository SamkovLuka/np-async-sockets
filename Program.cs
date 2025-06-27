using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Collections.Generic;
namespace async_client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("192.168.1.3");
            int port = 5555;

            while (true)
            {
                Console.Write("Press Enter to get a quote or type 'exit': ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = new IPEndPoint(ip, port);
                client.Connect(endPoint);

                byte[] buffer = new byte[1024];
                int bytesRead = client.Receive(buffer);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Quote: " + message + "\n");

                client.Close();
            }

            Console.WriteLine("Client closed.");
        }
    }
}
