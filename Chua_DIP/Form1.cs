using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chua_IP1
{
    public partial class Main : Form
    {
        Bitmap loaded, processed;
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for(int y = 0; y < loaded.Height; y++)
                {
                    Color c = loaded.GetPixel(x, y);
                    int grey = (c.R + c.G + c.B) / 3;
                    Color greyscale = Color.FromArgb(grey, grey, grey);
                    processed.SetPixel(x, y, greyscale);
                }
            }
            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color c = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] histogram = new int[256];
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color c = loaded.GetPixel(x, y);
                    int grey = (c.R + c.G + c.B) / 3;
                    histogram[grey]++;
                }
            }

            //find the maximum value in the histogram
            int max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > max)
                {
                    max = histogram[i];
                }
            }

            //create a new image to display the histogram
            Bitmap histogramImage = new Bitmap(256, 256);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (j < 256 - (histogram[i] * 256 / max))
                    {
                        histogramImage.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        histogramImage.SetPixel(i, j, Color.Black);
                    }
                }
            }
            pictureBox2.Image = histogramImage;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color c = loaded.GetPixel(x, y);
                    int red = (int)(c.R * 0.393 + c.G * 0.769 + c.B * 0.189);
                    int green = (int)(c.R * 0.349 + c.G * 0.686 + c.B * 0.168);
                    int blue = (int)(c.R * 0.272 + c.G * 0.534 + c.B * 0.131);
                    if (red > 255) red = 255;
                    if (green > 255) green = 255;
                    if (blue > 255) blue = 255;
                    processed.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }
            pictureBox2.Image = processed;
        }

        private void subtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubtractImage si = new SubtractImage();
            si.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.Image = processed;
            processed.Save(saveFileDialog1.FileName);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    Color c = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, c);
                }
            }
            pictureBox2.Image = processed;
        }

        public Main()
        {
            InitializeComponent();
        }        
    }
}
