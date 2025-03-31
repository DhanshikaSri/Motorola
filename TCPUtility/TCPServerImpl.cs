using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motorola
{
    public class TCPServerImpl
    {
        private static TcpListener listener = null;
        private static List<TcpClient> connectedClients = new List<TcpClient>();
        TcpClient client = null;
        NetworkStream stream = null;
        private int port = int.MinValue;
        private string IP = string.Empty;
        private bool isRunning = true;

        public TCPServerImpl () { }

        public TCPServerImpl(string IP ,int port)
        {
            this.IP = IP;
            this.port = port;
            if (listener == null)
            {
                listener = new TcpListener(IPAddress.Parse(IP), port);
                listener.Start();
                TCPManager.ConnectedServerPort = port;
                TCPManager.ConnectedServerIP = IP;
                TCPManager.IsServerConnected = true;
                TCPManager.IsTCPServer = true;
                ParentMessageHandler.LoadUserControlCompleted(TCP.Server);
            }
           
        }

        public void Start()
        {
            try
            {
              
                handleMessage("Server started. Listening for clients...");               

                // Run client connection handling on a separate thread
                Task.Run(() => AcceptClientsAsync());

                // Main thread is free for other tasks
                while (isRunning)
                {
                    Thread.Sleep(1000); // Simulating other tasks in main thread
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
               
            }            
        }

        private async Task AcceptClientsAsync()
        {
            while (isRunning)
            {
                try
                {
                    // Accept client asynchronously (Non-blocking)
                    if (listener == null)
                        return;
                    client = await listener.AcceptTcpClientAsync();
                    lock (connectedClients)
                    {
                        connectedClients.Add(client); // Store new client
                    }
                    handleMessage("Client connected:");


                    // Handle client in a new task (so it doesn't block other connections)
                    _ = Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    handleMessage($"Error: {ex.Message}");
                }
            }
        }

        private async Task HandleClient(TcpClient client)
        {
             stream = client.GetStream();
            byte[] buffer = new byte[1024];

            // Start sending heartbeats
            _ = Task.Run(() => SendHeartbeats(client));

            try
            {
                while (client.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                   handleMessage($"Received: {message}");

                    // Process request and send ACK
                    string response = "ACK";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    handleMessage("ACK sent.");
                }
            }
            catch (Exception ex)
            {
                handleMessage($"Client disconnected: {ex.Message}");
            }
            finally
            {
                lock (connectedClients)
                {
                    connectedClients.Remove(client); // Remove disconnected client
                }
                client.Close();
                handleMessage("Client connection closed.");
            }
        }

        private async Task SendHeartbeats(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            while (client.Connected)
            {
                try
                {
                    string heartbeat = "HEARTBEAT";
                    byte[] heartbeatBytes = Encoding.UTF8.GetBytes(heartbeat);
                    await stream.WriteAsync(heartbeatBytes, 0, heartbeatBytes.Length);
                    handleMessage("Heartbeat sent.");

                    await Task.Delay(5000); // Wait 30 seconds
                }
                catch (Exception)
                {
                    handleMessage("Heartbeat failed. Client disconnected.");
                    break;
                }
            }
        }

        public  async Task SendMessage(TcpClient client, string message)
        {
            try
            {
                if (client.Connected)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await client.GetStream().WriteAsync(data, 0, data.Length);
                    handleMessage($"Sent to {client.Client.RemoteEndPoint}: {message}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to send message to {client.Client.RemoteEndPoint}");
            }
        }

        public async Task BroadcastMessage(string message)
        {
            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    _ = SendMessage(client, message); // Send message to all clients
                }
            }
        }

        private void handleMessage(string Message)
        {
            try
            {
                ParentMessageHandler.SendMessageToUserControl(Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void StopServer()
        {
            try
            {
                isRunning = false;
                listener.Stop();
                listener = null;
                stream = null;               
                lock (connectedClients)
                {
                    connectedClients.Remove(client); // Remove disconnected client
                }
                TCPManager.ConnectedServerPort = int.MinValue;
                TCPManager.ConnectedServerIP = string.Empty;
                TCPManager.IsServerConnected = false;
                TCPManager.IsTCPServer = false;
                ParentMessageHandler.LoadUserControlCompleted(TCP.None);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
               
            }
        }
    }
}
