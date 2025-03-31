using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            ParentMessageHandler.OnloadUserControl += ParentMessageHandler_OnloadUserControl;
           
        }

       

        private void ParentMessageHandler_OnloadUserControl(bool isServer)
        {
            try
            {
                if (isServer)
                {
                    controlToLoad = new UsrTCPServerctrl(); 
                    controlToLoad.Dock = DockStyle.Fill;
                    panel1.Controls.Add(controlToLoad);
                } 
                else if (!isServer && controlToLoad!=null)
                {
                    panel1.Controls.Remove(controlToLoad);
                    panel1.Controls.Clear();
                }
            }
            catch (Exception)
            {
                throw;
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
    }
}

