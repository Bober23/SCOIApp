using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SCOI
{
	public static class ImageProcessor
	{
		public static System.Drawing.Image SumLayers(System.Drawing.Image mainImage, Layer subLayer)
		{

			Bitmap mainBmp = new Bitmap(mainImage);
			Bitmap newBmp = new Bitmap(mainBmp.Width, mainBmp.Height);
			Bitmap subBmp = new Bitmap(subLayer.Image, new System.Drawing.Size(mainBmp.Width, mainBmp.Height));
			for (int i = 0; i < mainBmp.Width; i++)
			{
				for (int j = 0; j < mainBmp.Height; j++)
				{
					var mainPixel = mainBmp.GetPixel(i, j);
					var subPixel = subBmp.GetPixel(i, j);
					int rChannel = mainPixel.R;
					int gChannel = mainPixel.G;
					int bChannel = mainPixel.B;
					if (subLayer.R)
					{
						rChannel = (int)(mainPixel.R + subPixel.R * ((100.0001 - subLayer.Transparency) / 100)) / 2;
						rChannel = Clamp(rChannel, 0, 255);
					}
					if (subLayer.G)
					{
						gChannel = (int)(mainPixel.G + subPixel.G * ((100.0001 - subLayer.Transparency) / 100)) / 2;
						gChannel = Clamp(gChannel, 0, 255);
					}
					if (subLayer.B)
					{
						bChannel = (int)(mainPixel.B + subPixel.B * ((100.0001 - subLayer.Transparency) / 100)) / 2;
						bChannel = Clamp(bChannel, 0, 255);
					}
					System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(rChannel, gChannel, bChannel);
					newBmp.SetPixel(i, j, newPixel);
				}
			}
			return newBmp;
		}
		public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) return min;
			else if (val.CompareTo(max) > 0) return max;
			else return val;
		}
	}
}
