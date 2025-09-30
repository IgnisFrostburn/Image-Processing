using AForge.Video.DirectShow;
using AForge.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Image_Processing
{
    public class Tab2_Webcam
    {
        private static FilterInfoCollection videoDevices;
        private static VideoCaptureDevice videoSource;
        private static PictureBox pictureBox;
        private static PictureBox filteredPictureBox;
        public enum webCamFilter
        {
            None,
            Copy,
            Greyscale,
            Inverted,
            Histogram,
            Sepia
        }
        public static webCamFilter currentFilter = webCamFilter.None;

        private static void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap original = (Bitmap)eventArgs.Frame.Clone();
                
                if (pictureBox.Image != null) pictureBox.Image.Dispose();
                pictureBox.Image = (Bitmap)original.Clone();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                if (filteredPictureBox != null)
                {
                    Bitmap processed = applyFilter(original);
                    if (filteredPictureBox.Image != null) filteredPictureBox.Image.Dispose();
                    filteredPictureBox.Image = processed;
                    filteredPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }

                original.Dispose();
            }
            catch { }
        }

        public static void turnOnCamera(PictureBox picture, PictureBox picture2)
        {
            pictureBox = picture;
            filteredPictureBox = picture2;

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No webcam found!");
                return;
            }

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            videoSource.Start();
        }

        public static void turnOffCamera()
        {
            if (videoSource != null)
            {
                if (videoSource.IsRunning)
                {
                    videoSource.Stop();
                }
                videoSource.NewFrame -= videoSource_NewFrame;
                videoSource = null;
            } 
        }

        private static Bitmap applyFilter(Bitmap input)
        {
            switch (currentFilter)
            {
                case webCamFilter.Copy:
                    return (Bitmap)input.Clone();

                case webCamFilter.Greyscale:
                    return Tab1_ImageProcessing.greyscaleImage(input);

                case webCamFilter.Inverted:
                    return Tab1_ImageProcessing.invertColor(input);

                case webCamFilter.Histogram:
                    return Tab1_ImageProcessing.createHistogram(input);

                case webCamFilter.Sepia:
                    return Tab1_ImageProcessing.sepiaImage(input);

                default:
                    return (Bitmap)input.Clone();
            }
        }
    }
}
