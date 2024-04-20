
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SCOI
{
    public static class ImageProcessor
    {
        public static System.Drawing.Image SumLayers(System.Drawing.Image mainImage, Layer subLayer)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            byte[] subBytes = Converter.FromBitmapToByte((Bitmap)subLayer.Image, targetWidth: mainImage.Width, targetHeight: mainImage.Height);
            double transparencyCoef = 1 - subLayer.Transparency / 100;
            for (int i = 0; i < mainBytes.Length; i++)
            {
                if (subLayer.B)
                {
                    int b = (int)(mainBytes[i] + subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(b, 0, 255);
                }
                i++;
                if (subLayer.G)
                {
                    int g = (int)(mainBytes[i] + subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(g, 0, 255);
                }
                i++;
                if (subLayer.R)
                {
                    int r = (int)(mainBytes[i] + subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(r, 0, 255);
                }
            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);

        }
        public static System.Drawing.Image DivLayers(System.Drawing.Image mainImage, Layer subLayer)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            byte[] subBytes = Converter.FromBitmapToByte((Bitmap)subLayer.Image, targetWidth: mainImage.Width, targetHeight: mainImage.Height);
            double transparencyCoef = 1 - subLayer.Transparency / 100;
            for (int i = 0; i < mainBytes.Length; i++)
            {
                if (subLayer.B)
                {
                    int b = (int)(mainBytes[i] - subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(b, 0, 255);
                }
                i++;
                if (subLayer.G)
                {
                    int g = (int)(mainBytes[i] - subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(g, 0, 255);
                }
                i++;
                if (subLayer.R)
                {
                    int r = (int)(mainBytes[i] - subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(r, 0, 255);
                }
            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);

        }
        public static System.Drawing.Image MiddleLayers(System.Drawing.Image mainImage, Layer subLayer)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            byte[] subBytes = Converter.FromBitmapToByte((Bitmap)subLayer.Image, targetWidth: mainImage.Width, targetHeight: mainImage.Height);
            double transparencyCoef = 1 - subLayer.Transparency / 100;
            for (int i = 0; i < mainBytes.Length; i++)
            {
                if (subLayer.B)
                {
                    int b = (int)(mainBytes[i] * (2 - transparencyCoef) + subBytes[i] * transparencyCoef) / 2;
                    mainBytes[i] = (byte)Clamp(b, 0, 255);
                }
                i++;
                if (subLayer.G)
                {
                    int g = (int)(mainBytes[i] * (2 - transparencyCoef) + subBytes[i] * transparencyCoef) / 2;
                    mainBytes[i] = (byte)Clamp(g, 0, 255);
                }
                i++;
                if (subLayer.R)
                {
                    int r = (int)(mainBytes[i] * (2 - transparencyCoef) + subBytes[i] * transparencyCoef) / 2;
                    mainBytes[i] = (byte)Clamp(r, 0, 255);
                }
            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);

        }
        public static System.Drawing.Image MaxLayers(System.Drawing.Image mainImage, Layer subLayer)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            byte[] subBytes = Converter.FromBitmapToByte((Bitmap)subLayer.Image, targetWidth: mainImage.Width, targetHeight: mainImage.Height);
            double transparencyCoef = 1 - subLayer.Transparency / 100;
            for (int i = 0; i < mainBytes.Length; i++)
            {
                if (subLayer.B)
                {
                    int b = (int)Math.Max(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(b, 0, 255);
                }
                i++;
                if (subLayer.G)
                {
                    int g = (int)Math.Max(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(g, 0, 255);
                }
                i++;
                if (subLayer.R)
                {
                    int r = (int)Math.Max(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(r, 0, 255);
                }
            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);

        }
        public static System.Drawing.Image MinLayers(System.Drawing.Image mainImage, Layer subLayer)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            byte[] subBytes = Converter.FromBitmapToByte((Bitmap)subLayer.Image, targetWidth: mainImage.Width, targetHeight: mainImage.Height);
            double transparencyCoef = 1 - subLayer.Transparency / 100;
            for (int i = 0; i < mainBytes.Length; i++)
            {
                if (subLayer.B)
                {
                    int b = (int)Math.Min(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(b, 0, 255);
                }
                i++;
                if (subLayer.G)
                {
                    int g = (int)Math.Min(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(g, 0, 255);
                }
                i++;
                if (subLayer.R)
                {
                    int r = (int)Math.Min(mainBytes[i], subBytes[i] * transparencyCoef);
                    mainBytes[i] = (byte)Clamp(r, 0, 255);
                }
            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);

        }
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static System.Drawing.Image CalculateGradationTranform(System.Drawing.Image mainImage, List<MyPoint> gradationPoints)
        {
            gradationPoints = gradationPoints.OrderBy(x => x.x).ToList();
            List<double> xList = new List<double>();
            List<double> yList = new List<double>();
            foreach (MyPoint point in gradationPoints)
            {
                xList.Add(Clamp(point.x, 0, 255));
                yList.Add(Clamp(point.y, 0, 255));
            }
            CubicSpline spline = new CubicSpline();
            spline.BuildSpline(xList.ToArray(), yList.ToArray(), xList.Count);

            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            for (int i = 0; i < mainBytes.Length; i++)
            {

                int b = (int)spline.Interpolate(mainBytes[i]);
                mainBytes[i] = (byte)Clamp(b, 0, 255);

                i++;

                int g = (int)spline.Interpolate(mainBytes[i]);
                mainBytes[i] = (byte)Clamp(g, 0, 255);

                i++;

                int r = (int)spline.Interpolate(mainBytes[i]);
                mainBytes[i] = (byte)Clamp(r, 0, 255);

            }
            return Converter.FromByteToBitmap(mainBytes, mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);
        }
        public static double[] CalculateDataset(System.Drawing.Image mainImage)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            List<byte> monochromeBytes = new List<byte>();
            for (int i = 0; i < mainBytes.Length-2; i+=3)
            {
                monochromeBytes.Add((byte)Math.Round((double)(mainBytes[i] + mainBytes[i + 1] + mainBytes[i + 2]) / 3));
            }
            double[] data = new double[256];
            for (int i = 0; i < monochromeBytes.Count; i++)
            {
                data[monochromeBytes[i]]++;
            }
            return data;
        }
        public static System.Drawing.Image BinarizeGavrilov(System.Drawing.Image mainImage)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)mainImage);
            List<byte> monochromeBytes = new List<byte>();
            double middlePixelValue = 0;
            for (int i = 0; i < mainBytes.Length - 2; i += 3)
            {
                monochromeBytes.Add((byte)Math.Round((double)(mainBytes[i] + mainBytes[i + 1] + mainBytes[i + 2]) / 3));
                monochromeBytes.Add((byte)Math.Round((double)(mainBytes[i] + mainBytes[i + 1] + mainBytes[i + 2]) / 3));
                monochromeBytes.Add((byte)Math.Round((double)(mainBytes[i] + mainBytes[i + 1] + mainBytes[i + 2]) / 3));
                middlePixelValue += monochromeBytes.Last();
            }
            int pixelCount = mainImage.Width * mainImage.Height;
            middlePixelValue = middlePixelValue / pixelCount;
            for (int i = 0; i < monochromeBytes.Count; i++)
            {
                if (monochromeBytes[i] <= middlePixelValue)
                {
                    monochromeBytes[i] = 0;
                }
                else
                {
                    monochromeBytes[i] = 255;
                }
            }
            return Converter.FromByteToBitmap(monochromeBytes.ToArray(), mainImage.Width, mainImage.Height, mainImage.HorizontalResolution, mainImage.VerticalResolution);
        }
        public static double[,] GenerateGaussMatrix(double sigma, int radius)
        {
            double sum = 0;
            double[,] gauss = new double[radius * 2 + 1, radius * 2 + 1];
            for (int i = 0; i< radius*2+1; i++)
            {
                for (int j = 0; j< radius * 2 + 1; j++)
                {
                    double value = 1.0 / (2.0 * Math.PI * Math.Pow(sigma, 2)) * Math.Exp(-1.0 * (Math.Pow(i-radius, 2) + Math.Pow(j-radius, 2)) / (2.0 * Math.Pow(sigma, 2)));
                    gauss[i, j] = value;
                    
                }
            }
            return gauss;
        }
        public static System.Drawing.Image UseFilterOnImage(System.Drawing.Image image, double[,] filter, int r)
        {
            byte[] mainBytes = Converter.FromBitmapToByte((Bitmap)image);
            MyPixel[,] pixels = new MyPixel[image.Height, image.Width];
            int counter = 0;
            for (int i = 0; i < image.Height; i++) 
            {
                for (int j = 0; j < image.Width; j++)
                {
                    MyPixel pixel = new MyPixel();
                    pixel.R = mainBytes[counter];
                    counter++;
                    pixel.G = mainBytes[counter];
                    counter++;
                    pixel.B = mainBytes[counter];
                    counter++;
                    pixels[i, j] = pixel;
                }
            }
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0;j < image.Width; j++)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;
                    for (int k = 0; k < r*2+1; k++)
                    {
                        for (int l = 0; l < r*2+1; l++)
                        {
                            var filterValue = filter[k, l];
                            int p1;
                            int p2;
                            if (i - r + k < 0)
                            {
                                p1 = (i - r + k)*-1;
                            }
                            else if (i - r + k >= image.Height)
                            {
                                p1 = image.Height-1-((i - r + k)-image.Height);
                            }
                            else
                            {
                                p1 = i-r+k;
                            }
                            if (j - r + l < 0)
                            {
                                p2 = (j - r + l) * -1;
                            }
                            else if (j - r + l >= image.Width)
                            {
                                p2 = image.Width-1 - ((j - r + l) - image.Width);
                            }
                            else
                            {
                                p2 = j - r + l;
                            }
                            sumR += filterValue * pixels[p1, p2].R;
                            sumG += filterValue * pixels[p1, p2].G;
                            sumB += filterValue * pixels[p1, p2].B;
                        }
                    }
                    pixels[i,j].R = (int)sumR;
                    pixels[i, j].G = (int)sumG;
                    pixels[i, j].B = (int)sumB;
                }
            }
            byte[] newBytes = new byte[mainBytes.Length];
            counter = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++) 
                {
                    newBytes[counter] = (byte)pixels[i, j].R;
                    counter++;
                    newBytes[counter] = (byte)pixels[i, j].G;
                    counter++;
                    newBytes[counter] = (byte)pixels[i, j].B;
                    counter++;
                }
            }
            return Converter.FromByteToBitmap(newBytes, image.Width, image.Height, image.HorizontalResolution, image.VerticalResolution);
        }
        struct MyPixel
        {
            public int R;
            public int G;
            public int B;
        }
    }
}
