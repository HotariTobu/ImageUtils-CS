using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorCounter
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public Color ButtonColor { get => button1.BackColor; set => button1.BackColor = value; }
        public string LabelText { get => label1.Text; set => label1.Text = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LabelText);
        }
    }
}
