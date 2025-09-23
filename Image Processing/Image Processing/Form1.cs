using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select an image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Select an image";
            openFileDialog2.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.Image = new Bitmap(openFileDialog2.FileName);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap original = new Bitmap(pictureBox1.Image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        copy.SetPixel(x, y, pixelColor);
                    }
                }

                pictureBox3.Image = copy;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap original = new Bitmap(pictureBox1.Image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        int greyPixel = (pixelColor.R + pixelColor.G + pixelColor.B)/3;
                        Color greyColorPixel = Color.FromArgb(greyPixel, greyPixel, greyPixel);
                        copy.SetPixel(x,y, greyColorPixel);
                    }
                }

                pictureBox3.Image = copy;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap original = new Bitmap(pictureBox1.Image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        Color invertedColorPixel = Color.FromArgb(255-pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                        copy.SetPixel(x, y, invertedColorPixel);
                    }
                }

                pictureBox3.Image = copy;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap original = new Bitmap(pictureBox1.Image);
                int[] histogram = new int[256];
                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        int greyPixel = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        histogram[greyPixel]++;
                    }
                }

                int width = 256;
                int height = 100;

                Bitmap histImage = new Bitmap(width, height);

                int max = histogram.Max();

                for (int x = 0; x < width; x++)
                {
                    int value = (int)((histogram[x] / (float)max) * height);

                    for (int y = height - 1; y >= height - value; y--)
                    {
                        histImage.SetPixel(x, y, Color.Black);
                    }
                }

                pictureBox3.Image = histImage;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap original = new Bitmap(pictureBox1.Image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        int red = (int)((0.393*pixelColor.R) + (0.769*pixelColor.G) + (0.189*pixelColor.B));
                        int green = (int)((0.349*pixelColor.R) + (0.686*pixelColor.G) + (0.168*pixelColor.B));
                        int blue = (int)((0.272*pixelColor.R ) + (0.534*pixelColor.G) + (0.131*pixelColor.B));
                        red = Math.Min(255, red);
                        green = Math.Min(255, green);
                        blue = Math.Min(255, blue);
                        Color newColor = Color.FromArgb(red, green, blue);
                        copy.SetPixel(x, y, newColor);
                    }
                }

                pictureBox3.Image = copy;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("No image to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Image As";
            saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null) 
            {
                Bitmap imageB = new Bitmap(pictureBox2.Image);
                Bitmap imageA = new Bitmap(pictureBox1.Image);
                Bitmap resultImage = new Bitmap(imageB.Width, imageB.Height);

                Color myGreen = Color.FromArgb(0, 255, 0);
                int threshold = 50;

                for (int x = 0; x < imageB.Width;x++)
                {
                    for (int y = 0; y < imageB.Height; y++)
                    {
                        Color pixel = imageA.GetPixel(x, y);
                        Color backPixel = imageB.GetPixel(x % imageB.Width, y % imageB.Height);
                        if (pixel.G > pixel.R + threshold && pixel.G > pixel.B + threshold)
                        {
                            resultImage.SetPixel(x, y, backPixel);
                        }
                        else
                        {
                            resultImage.SetPixel(x, y, pixel);
                        }
                    }
                }
                pictureBox3.Image = resultImage;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
