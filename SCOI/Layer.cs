using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCOI
{
	public class Layer
	{
		public System.Drawing.Image Image { get; set; }
		public string Operation { get; set; }
		public bool R {  get; set; }
		public bool G { get; set; }
		public bool B { get; set; }
		
		public double Transparency { get; set; }

		public Layer(System.Drawing.Image image) 
		{
			Image = image;
			Operation = "Не учитывать";
			R = true;
			G = true;
			B = true;
		}
		
	}

}
