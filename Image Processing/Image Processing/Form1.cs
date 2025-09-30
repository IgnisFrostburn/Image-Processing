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
using AForge.Video;
using AForge.Video.DirectShow;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        private const string imageType = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tab2_Webcam.turnOffCamera();
        }

        private void turnOnCamera(object sender, EventArgs e)
        {
            Tab2_Webcam.turnOnCamera(pictureBox6, pictureBox4);
        }
        

        //IMAGE PROCESSING
        private void loadImage1(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select an image";
            openFileDialog1.Filter = imageType;
            openFileDialog1.ShowDialog();
        }

        private void loadImage2(object sender, EventArgs e)
        {
            openFileDialog2.Title = "Select an image";
            openFileDialog2.Filter = imageType;
            openFileDialog2.ShowDialog();
        }

        private void loadImage3(object sender, EventArgs e)
        {
            openFileDialog3.Title = "Select an image";
            openFileDialog3.Filter = imageType;
            openFileDialog3.ShowDialog();
        }

        private void loadImage4(object sender, EventArgs e)
        {
            openFileDialog4.Title = "Select an image";
            openFileDialog4.Filter = imageType;
            openFileDialog4.ShowDialog();
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

        private void openFileDialog3_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox9.Image = new Bitmap(openFileDialog3.FileName);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void openFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox5.Image = new Bitmap(openFileDialog4.FileName);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void makeBasicCopy(object sender, EventArgs e)
        {
            pictureBox3.Image = Tab1_ImageProcessing.makeBasicCopy(new Bitmap(pictureBox1.Image));
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void greyscaleImage(object sender, EventArgs e)
        {
            pictureBox3.Image = Tab1_ImageProcessing.greyscaleImage(new Bitmap(pictureBox1.Image));
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void invertColor(object sender, EventArgs e)
        {
            pictureBox3.Image = Tab1_ImageProcessing.invertColor(new Bitmap(pictureBox1.Image));
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void createHistogram(object sender, EventArgs e)
        {
            pictureBox3.Image = Tab1_ImageProcessing.createHistogram(new Bitmap(pictureBox1.Image));
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void sepiaImage(object sender, EventArgs e)
        {
            pictureBox3.Image = Tab1_ImageProcessing.sepiaImage(new Bitmap(pictureBox1.Image));
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
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

        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            if (pictureBox7.Image != null)
            {
                pictureBox7.Image.Save(saveFileDialog2.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                MessageBox.Show("No image to save!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveImage(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Image As";
            saveFileDialog1.Filter = imageType;
            saveFileDialog1.ShowDialog();
        }

        private void saveImage2(object sender, EventArgs e)
        {
            saveFileDialog2.Title = "Save Image As";
            saveFileDialog2.Filter = imageType;
            saveFileDialog2.ShowDialog();
        }

        private void subtractImage(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null && pictureBox1.Image != null)
            {
                pictureBox3.Image = Tab1_ImageProcessing.subtractImage(new Bitmap(pictureBox1.Image), new Bitmap(pictureBox2.Image));
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            } 
            else
            {
                MessageBox.Show("Error! Both Images must be uploaded");
            }
        }

        //WEBCAM


        private void copyWebcam(object sender, EventArgs e)
        {
            Tab2_Webcam.currentFilter = Tab2_Webcam.webCamFilter.Copy;
        }

        private void greyscaleWebcam(object sender, EventArgs e)
        {
            Tab2_Webcam.currentFilter = Tab2_Webcam.webCamFilter.Greyscale;
        }

        private void invertedWebCam(object sender, EventArgs e)
        {
            Tab2_Webcam.currentFilter = Tab2_Webcam.webCamFilter.Inverted;
        }

        private void histogramWebCam(object sender, EventArgs e)
        {
            Tab2_Webcam.currentFilter = Tab2_Webcam.webCamFilter.Histogram;
        }

        private void sepiaWebcam(object sender, EventArgs e)
        {
            Tab2_Webcam.currentFilter = Tab2_Webcam.webCamFilter.Sepia;
        }

        //CONVOLUTION MATRIX   
        private void smoothImage(object sender, EventArgs e)
        {
            if(pictureBox9.Image == null)
            {
                MessageBox.Show("Error! Image must be uploaded");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.smoothImage(bmp,1);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void gaussianBlurImage(object sender, EventArgs e)
        {
            if (pictureBox9.Image == null)
            {
                MessageBox.Show("Error! Image must be uploaded");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.gaussianBlurImage(bmp, 1);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void sharpenImage(object sender, EventArgs e)
        {
            if (pictureBox9.Image == null)
            {
                MessageBox.Show("Error! Image must be uploaded");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.sharpenImage(bmp, 1);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void meanRemovalImage(object sender, EventArgs e)
        {
            if (pictureBox9.Image == null)
            {
                MessageBox.Show("Error! Image must be uploaded");
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.meanRemovalImage(bmp, 1);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void embossImage(object sender, EventArgs e)
        {
            if (pictureBox9.Image == null)
            {
                MessageBox.Show("Error! Image must be uploaded");
                return;
            }
            contextMenuStrip1.Show(button20, new Point(0, button20.Height));
        }

        private void laplacianEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 1);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void horzVertEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 2);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void allDirectionEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 3);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void lossyEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 4);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void horizontalOnlyEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 5);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void verticalOnlyEmbossMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox9.Image);
            ConvMatrix.embossImage(bmp, 1, 6);
            pictureBox7.Image = bmp;
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
