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
    public partial class UsrTCPServerctrl : UserControl
    {

        public UsrTCPServerctrl()
        {
            InitializeComponent();

            ParentMessageHandler.evtsendmessage += ParentMessageHandler_evtsendmessage;
        }

        private void ParentMessageHandler_evtsendmessage(string message)
        {
            try
            {
                if (richTextBox1.InvokeRequired)
                {
                    richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(message + Environment.NewLine)));
                }
                else
                {
                    richTextBox1.AppendText(message + Environment.NewLine);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TCPServerImpl tCPServerImpl = new TCPServerImpl();
                _ = tCPServerImpl.BroadcastMessage(richTextBox2.Text);
                richTextBox2.Text = "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
