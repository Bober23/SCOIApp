﻿using System;
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
    }
}
