using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFMpegCore;

namespace HighlightProcessor
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var DialogR = openFileDialog1.ShowDialog();
            if (DialogR == DialogResult.OK)
            {
                var FileName = openFileDialog1.FileName;
                try
                {
                    textBox1.Text = System.IO.Path.GetFullPath(FileName);
                }
                catch
                {
                    MessageBox.Show("Couldn't find the file path. Try entering it manually.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var DialogR = openFileDialog2.ShowDialog();
            if (DialogR == DialogResult.OK)
            {
                var FileName = openFileDialog2.FileName;
                try
                {
                    textBox2.Text = System.IO.Path.GetFullPath(FileName);
                }
                catch
                {
                    MessageBox.Show("Couldn't find the file path. Try entering it manually.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FFMpeg.Join(textBox3.Text, textBox1.Text, textBox2.Text);
            MessageBox.Show("Job done.");
        }
    }
}
