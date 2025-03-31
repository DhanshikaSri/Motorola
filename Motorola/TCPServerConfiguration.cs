using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public partial class TCPServerConfiguration : Form
    {
        public TCPServerConfiguration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
                {
                    using (CustomMessageBox customMessageBox = new CustomMessageBox("IP Address & Port Number is not available!", "Message"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
                else if (string.IsNullOrEmpty(textBox1.Text))
                {
                    using (CustomMessageBox customMessageBox = new CustomMessageBox("IPAddress is not available!", "Message"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
                else if (string.IsNullOrEmpty(textBox2.Text))
                {
                    using (CustomMessageBox customMessageBox = new CustomMessageBox("Port Number is not available!", "Message"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
                else if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    if (!IsValidIPAddress(textBox1.Text))
                    {
                        using (CustomMessageBox customMessageBox = new CustomMessageBox("Invalid IP Address!", "Message"))
                        {
                            customMessageBox.ShowDialog();
                        }
                    }
                    if (!IsValidPort(textBox2.Text))

                    {
                        using (CustomMessageBox customMessageBox = new CustomMessageBox("Invalid Port! Please enter a number between 1 and 65535.", "Message"))
                        {
                            customMessageBox.ShowDialog();
                        }
                        textBox2.Clear();
                    }
                    StartServer();
                }
            }
            catch (Exception ex)
            {
                using (CustomMessageBox customMessageBox = new CustomMessageBox(ex.Message, "Message"))
                {
                    customMessageBox.ShowDialog();
                }
            }
            finally
            {
                if (TCPManager.IsServerConnected)
                {
                    label3.Text = "Server Connected";
                    button3.Enabled = true;
                    button3.Visible = true;
                    button3.BackColor = Color.Green;
                }
            }
        }

        private async void StartServer()
        {
            try
            {
                ServerConfiguration ObjserverConfiguration = null;
                TCPServerImpl objtCPServerImpl = null;
                ObjserverConfiguration = new ServerConfiguration();
                ObjserverConfiguration.IPAddress = textBox1.Text.Trim();
                ObjserverConfiguration.PortNumber = int.Parse(textBox2.Text.Trim());

                objtCPServerImpl = new TCPServerImpl(ObjserverConfiguration.IPAddress, ObjserverConfiguration.PortNumber);
              
                await Task.Run(() => objtCPServerImpl.Start());
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsValidIPAddress(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }
        private bool IsValidPort(string input)
        {
            return int.TryParse(input, out int port) && port >= 1 && port <= 65535;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = "Closing the TCP Configuration....";
                await Task.Delay(500);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void TCPServerConfiguration_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Visible = false;
            button3.ForeColor = Color.White;
            button3.BackColor = Color.Red;
            if (ValidateServerConnected())
            {
                this.Close();
            }
        }

        private bool ValidateServerConnected()
        {
            bool Isconnected = false;
            try
            {
                if (TCPManager.IsServerConnected)
                {
                    string Message = string.Format("The server is already connected with IP : {0} Port : {1}",
                                TCPManager.ConnectedServerIP, TCPManager.ConnectedServerPort);

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(Message, "Information"))
                    {
                        customMessageBox.ShowDialog();
                    }

                    Isconnected = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Isconnected;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {              
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
