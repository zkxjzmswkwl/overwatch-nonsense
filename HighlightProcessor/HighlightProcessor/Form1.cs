using FFMpegCore;
using FFMpegCore.Enums;
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

namespace HighlightProcessor
{
    struct Highlight
    {
        public TimeSpan Start;
        public TimeSpan Delta;
        public string Input;
        public string Output;
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ProcessInput(Highlight h)
        {
            //-------------------------------------------------------------------------
            // If operation ran, delete previous output before running new operation.
            // Configure ffmpeg options to output as:
            //          60      FPS
            //          LibX264 Video Codec
            //          AAC     Audio Codec
            //          21      CRF
            if (File.Exists(h.Output))
                File.Delete(h.Output);


            // ? this a bit much but I think is technically standard..?
            FFMpegArguments
                .FromFileInput(h.Input)
                .OutputToFile(h.Output, false, options => options
                    .Seek(h.Start)
                    .WithFramerate(60)
                    .WithDuration(h.Delta)
                    .WithVideoCodec(VideoCodec.LibX264)
                    .WithConstantRateFactor(21)
                    .WithAudioCodec(AudioCodec.Aac)
                    .WithVariableBitrate(4)
                    .WithVideoFilters(filterOptions => filterOptions
                        .Scale(VideoSize.Hd))
                    .WithFastStart())
                .ProcessSynchronously();

            MessageBox.Show(h.Output, "Job's done.", MessageBoxButtons.OK);
        }

        private Highlight FetchTimes()
        {

            //-------------------------------------------------------------------------
            // Set input to text box input, split to generate output path from input.
            Highlight highlight = new Highlight();
            highlight.Input = textBox1.Text;
            highlight.Output = highlight.Input.Split(".")[0] + "_output.mp4";

            if (textBox3.Text.Length > 1)
                highlight.Start = TimeSpan.Parse(textBox3.Text);
            if (textBox4.Text.Length > 1)
                highlight.Delta = TimeSpan.Parse(textBox4.Text) - highlight.Start;

            return highlight;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 2 || textBox3.Text.Length <= 2 || textBox4.Text.Length <= 2)
            {
                MessageBox.Show("Fill out all inputs before processing.", "noob", MessageBoxButtons.OK);
                return;
            }

            Highlight highlight = FetchTimes();
            ProcessInput(highlight);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 2)
            {
                MessageBox.Show("Must provide an input file for the intro/outro to be removed."
                    , "noob"
                    , MessageBoxButtons.OK);

                return;
            }

            //-------------------------------------------------------------------------
            // Overwatch highlights eat up 20 seconds with deadtime (intro/outro).
            // This removes both of those (6s intro, 14s outro). 
            Highlight highlight = FetchTimes();
            var FileInfo = FFProbe.Analyse(highlight.Input);
            highlight.Start = TimeSpan.Parse("00:00:06");
            highlight.Delta = FileInfo.Duration - TimeSpan.Parse("00:00:14");

            ProcessInput(highlight);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author: Carter\nDate: May 2018\nRevision Date: Nov. 2021", "About", MessageBoxButtons.OK); 
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

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Text = "00:00:05";
            textBox4.Text = "00:00:25";
        }
    }
}
