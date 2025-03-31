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
    public partial class CustomMessageBox: Form
    {      

        public CustomMessageBox(string Message,string Tittle)
        {
            InitializeComponent();

            label1.Text = Message.Trim();

            label1.TextAlign = ContentAlignment.MiddleCenter;

            this.Text = Tittle;

            this.StartPosition = FormStartPosition.CenterParent;

            CenterLabel();
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {

        }

        private void CenterLabel()
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 - 20; // Slightly above the center
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
