using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace SCOI
{
    public class MainLayoutModel
    {
        public List<Layer> layers = new List<Layer>();
		public List<MyPoint> points = new List<MyPoint>()
		{
			new MyPoint(0,0),
			new MyPoint(255,255)
		};
        public System.Drawing.Image MainImage { get; set; }

		public async Task OpenNewMainImage(IBrowserFile uploadedFile)
        {
			using Stream stream = uploadedFile.OpenReadStream(maxAllowedSize:10000000);
			MainImage = await Converter.FromStreamToImage(stream);
            layers.Clear();
            var mainLayer = new Layer(MainImage);
            mainLayer.Operation = "Основное изображение";
            layers.Add(mainLayer);
            Console.WriteLine("Vse");
        }
        public async Task LoadNewLayer(IBrowserFile uploadedFile)
        {
			using Stream stream = uploadedFile.OpenReadStream(maxAllowedSize: 10000000);
            var img = await Converter.FromStreamToImage(stream);
            layers.Add(new Layer(img));
            Console.WriteLine("Vse");
		}
        public void CalculateImage(string page)
        {
            if (page == "layers")
            {
				foreach (var layer in layers)
				{
					if (layer.Operation == "Сложение")
					{
						MainImage = ImageProcessor.SumLayers(layers.First().Image, layer);
					}
					if (layer.Operation == "Вычитание")
					{
						MainImage = ImageProcessor.DivLayers(layers.First().Image, layer);
					}
					if (layer.Operation == "Среднее")
					{
						MainImage = ImageProcessor.MiddleLayers(layers.First().Image, layer);
					}
					if (layer.Operation == "Максимум")
					{
						MainImage = ImageProcessor.MaxLayers(layers.First().Image, layer);
					}
					if (layer.Operation == "Минимум")
					{
						MainImage = ImageProcessor.MinLayers(layers.First().Image, layer);
					}
				}
			}
            if (page == "gradation")
			{
				MainImage = ImageProcessor.CalculateGradationTranform(layers.First().Image, points);
			}
        }

    }
}
