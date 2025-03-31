using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("192.168.1.6", 5000);
                NetworkStream stream = client.GetStream();

                Task.Run(() => ReceiveMessages(client)); // Start receiving data

                while (true)
                {
                    Console.Write("Enter message: ");
                    string message = Console.ReadLine();
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void ReceiveMessages(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Server: {response}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Disconnected from server.");
            }
        }
    }
}
