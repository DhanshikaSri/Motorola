using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public class TCPClientImpl
    {
        private static TcpClient _client;
        private static NetworkStream _stream;
        private bool _isRunning;

        public TCPClientImpl()
        {

        }

        public TCPClientImpl(string Ip, int Port)
        {
            try
            {
                if (_client == null)
                {
                    _client = new TcpClient(Ip, Port);
                    TCPManager.ConnectedClientPort = Port;
                    TCPManager.ConnectedClientIP = Ip;
                    TCPManager.IsClientConnected = true;
                    TCPManager.IsTCPClient = true;
                    ParentMessageHandler.LoadUserControlCompleted(TCP.Client);
                    handleMessage("Client Connected..");
                }
            }
            catch (Exception ex)
            {
                handleMessage("Error: {ex.Message} " + ex.Message);
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
        public  async Task SendMessage(string message)
        {
            try
            {
                handleMessage("Sending Message: " + message);
                if (_client == null || !_client.Connected) return;
                byte[] data = Encoding.UTF8.GetBytes(message);
                _stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                handleMessage("Error: {ex.Message} " + ex.Message);
            }
        }
        private async Task ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (_isRunning)
                {
                    try
                    {
                        int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break; // Connection closed
                        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        handleMessage($"Server response : {response}");
                    }
                    catch (Exception ex)
                    {
                        if (_isRunning)
                        {
                           handleMessage("Disconnected from server." + "Connection Lost" + ex.Message);
                        }
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ConnectClient()
        {
            try
            {
                handleMessage("Connecting Client");
                _stream = _client.GetStream();
                _isRunning = true;
                Task.Run(ReceiveMessages); // Start receiving messages asynchronously
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void StopClient()
        {
            try
            {
                _isRunning = false;
                _stream = null;
                _client = null;
                handleMessage("Client disconnected." + "Disconnected");
                ParentMessageHandler.LoadUserControlCompleted(TCP.None);
                TCPManager.IsClientConnected = false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
