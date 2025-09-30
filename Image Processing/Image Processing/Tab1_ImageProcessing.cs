using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processing
{
    public class Tab1_ImageProcessing
    {
        public static Bitmap makeBasicCopy(Bitmap image)
        {
            if (image != null)
            {
                Bitmap copy = new Bitmap(image.Width, image.Height);

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        Color pixelColor = image.GetPixel(x, y);
                        copy.SetPixel(x, y, pixelColor);
                    }
                }
                return copy;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        public static Bitmap greyscaleImage(Bitmap image)
        {
            if (image != null)
            {
                Bitmap original = new Bitmap(image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        int greyPixel = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        Color greyColorPixel = Color.FromArgb(greyPixel, greyPixel, greyPixel);
                        copy.SetPixel(x, y, greyColorPixel);
                    }
                }
                return copy;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        public static Bitmap invertColor(Bitmap image)
        {
            if (image != null)
            {
                Bitmap original = new Bitmap(image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        Color invertedColorPixel = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                        copy.SetPixel(x, y, invertedColorPixel);
                    }
                }
                return copy;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        public static Bitmap createHistogram(Bitmap image)
        {
            if (image != null)
            {
                Bitmap original = new Bitmap(image);
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

                return histImage;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        public static Bitmap sepiaImage(Bitmap image)
        {
            if (image != null)
            {
                Bitmap original = new Bitmap(image);
                Bitmap copy = new Bitmap(original.Width, original.Height);

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        Color pixelColor = original.GetPixel(x, y);
                        int red = (int)((0.393 * pixelColor.R) + (0.769 * pixelColor.G) + (0.189 * pixelColor.B));
                        int green = (int)((0.349 * pixelColor.R) + (0.686 * pixelColor.G) + (0.168 * pixelColor.B));
                        int blue = (int)((0.272 * pixelColor.R) + (0.534 * pixelColor.G) + (0.131 * pixelColor.B));
                        red = Math.Min(255, red);
                        green = Math.Min(255, green);
                        blue = Math.Min(255, blue);
                        Color newColor = Color.FromArgb(red, green, blue);
                        copy.SetPixel(x, y, newColor);
                    }
                }
                return copy;
            }
            else
            {
                MessageBox.Show("No image loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return null;
        }

        public static Bitmap subtractImage(Bitmap imageA, Bitmap imageB)
        {
            Bitmap resultImage = new Bitmap(imageB.Width, imageB.Height);

            Color myGreen = Color.FromArgb(0, 255, 0);
            int threshold = 50;

            for (int x = 0; x < imageB.Width; x++)
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
            return resultImage;
        }
    }
}
