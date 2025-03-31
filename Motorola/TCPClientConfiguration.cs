using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public partial class TCPClientConfiguration: Form
    {
        public TCPClientConfiguration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientConfiguration ObjserverConfiguration = null;

            TCPClientImpl objtCPServerImpl = null;
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
                    CustomMessageBox customMessageBox = new CustomMessageBox("Port Number is not available!", "Message");
                    {
                        customMessageBox.ShowDialog();
                    }
                }
                else
                {
                    ObjserverConfiguration  = new ClientConfiguration();
                    ObjserverConfiguration.IPAddress = textBox1.Text.Trim();
                    ObjserverConfiguration.PortNumber = int.Parse(textBox2.Text.Trim());

                    if(ObjserverConfiguration.IPAddress!=string.Empty && ObjserverConfiguration.PortNumber!=int.MinValue)
                    {
                        objtCPServerImpl = new TCPClientImpl();
                        objtCPServerImpl.ConnectServer(ObjserverConfiguration.IPAddress,ObjserverConfiguration.PortNumber);
                    }                    
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Close();
            }
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
           
        }
    }
}
