using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNG最適化
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void list_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect== DragDropEffects.Copy)
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (File.Exists(path))
                    {
                        list.Items.Add(path);
                    }
                }
            }
        }

        private void list_DragEnter(object sender, DragEventArgs e)
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

        private void outputPathBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                outputPathBox.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }
        }

        private void outputPathBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (Directory.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
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

        private void startButton_Click(object sender, EventArgs e)
        {
            foreach (string path in list.Items)
            {
                process.StartInfo.Arguments = $"-rem alla -l 9 \"{path}\" \"{outputPathBox.Text + "\\" + Path.GetFileName(path)}\"";
                process.Start();
            }

            list.Items.Clear();
        }
    }
}
