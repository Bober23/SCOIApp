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
        public System.Drawing.Image MainImage { get; set; }

        public async Task OpenNewMainImage(IBrowserFile uploadedFile)
        {
			Stream stream = uploadedFile.OpenReadStream();
			MainImage = await Converter.FromStreamToImage(stream);
            layers.Clear();
            var mainLayer = new Layer(MainImage);
            mainLayer.Operation = "Основное изображение";
            layers.Add(mainLayer);
		}
        public async Task LoadNewLayer(IBrowserFile uploadedFile)
        {
			Stream stream = uploadedFile.OpenReadStream();
            var img = await Converter.FromStreamToImage(stream);
            layers.Add(new Layer(img));
		}
        public void CalculateImage()
        {
            foreach (var layer in layers)
            {
                if (layer.Operation == "Сложение")
                {
                    MainImage = ImageProcessor.SumLayers(MainImage, layer);
                }
            }
        }

    }
}
