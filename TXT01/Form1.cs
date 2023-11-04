using System;
using System.IO;
using System.Windows.Forms;

namespace TXT01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBox1.SelectedItem as string);
        }

        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                listBox1.Items.AddRange(File.ReadAllLines(((string[])e.Data.GetData(DataFormats.FileDrop))[0]));
            }
        }

        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (File.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
