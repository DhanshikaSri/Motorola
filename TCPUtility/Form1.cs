using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public partial class Form1: Form
    {
        UserControl controlToLoad = null;
        public Form1()
        {
            InitializeComponent();

            ParentMessageHandler.OnLoadUserControl += ParentMessageHandler_OnLoadUserControl;
        }

        private void ParentMessageHandler_OnLoadUserControl(TCP tcpType)
        {
            try
            {
                panel1.Controls.Clear(); // Clear existing controls

                if (tcpType == TCP.Server)
                {
                    controlToLoad = new UsrTCPServerctrl();
                }
                else if (tcpType == TCP.Client)
                {
                    controlToLoad = new UsrTCPClientctrl();
                }
                else if (tcpType == TCP.None)
                {
                    RemoveUserControl(tcpType);
                }
                if (controlToLoad != null)
                {
                    controlToLoad.Dock = DockStyle.Fill;
                    panel1.Controls.Add(controlToLoad);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveUserControl(TCP tcpType)
        {
            try
            {
                if (tcpType == TCP.None && controlToLoad != null)
                {
                    panel1.Controls.Remove(controlToLoad);
                    controlToLoad.Dispose();
                    controlToLoad = null;
                }
                panel1.Controls.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                          
            }
            catch (Exception)
            {
                throw;
            }
        }        

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ParentMessageHandler.OnLoadUserControl -= ParentMessageHandler_OnLoadUserControl;
                Application.Exit();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void sTARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TCPServerConfiguration tCPServerConfiguration = null;
                try
                {                   
                    tCPServerConfiguration = new TCPServerConfiguration();
                    tCPServerConfiguration.StartPosition = FormStartPosition.CenterParent;
                    tCPServerConfiguration.ShowDialog();
                }
                catch (Exception)
                {
                    throw;
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

        private void sTOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TCPServerImpl tCPServerImpl = null;
            try     
            {
               
                if (TCPManager.IsServerConnected)
                {
                     tCPServerImpl = new TCPServerImpl(TCPManager.ConnectedServerIP,TCPManager.ConnectedServerPort);
                     tCPServerImpl.StopServer();

                    string Message = string.Format("Connection Closed");

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(Message, "Information"))
                    {
                        customMessageBox.ShowDialog();
                    }                    
                }
                else
                {
                    string Message = string.Format("There is no connection to close.");

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(Message, "Information"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
                tCPServerImpl = null;
            }       
        }

        // Client start
        private void sTARTToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            TCPClientConfiguration tCPClientConfiguration = null;
            try
            {
                tCPClientConfiguration = new TCPClientConfiguration();
                tCPClientConfiguration.StartPosition = FormStartPosition.CenterParent;
                tCPClientConfiguration.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Client Stop
        private void sTOPToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            TCPClientImpl tCPClientImpl = null;
            try
            {

                if (TCPManager.IsClientConnected)
                {
                    tCPClientImpl = new TCPClientImpl(TCPManager.ConnectedClientIP, TCPManager.ConnectedClientPort);
                    tCPClientImpl.StopClient();

                    string Message = string.Format("Connection Closed");

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(Message, "Information"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
                else
                {
                    string Message = string.Format("There is no connection to close.");

                    using (CustomMessageBox customMessageBox = new CustomMessageBox(Message, "Information"))
                    {
                        customMessageBox.ShowDialog();
                    }
                }
            }  
            catch (Exception)
            {
                throw;
            }
        }
    }
}

