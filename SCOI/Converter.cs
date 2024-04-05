using MudBlazor.Charts;
using System.Drawing;
namespace SCOI
{
	public static class Converter
	{
		public async static Task<string> FromStreamToImageSourceAsync(Stream stream)
		{
			byte[] bytes;
			using (var memoryStream = new MemoryStream())
			{
				await stream.CopyToAsync(memoryStream);
				bytes = memoryStream.ToArray();
			}

			string base64 = Convert.ToBase64String(bytes);
			return string.Format("data:image/jpg;base64, {0}", base64);
		}
		public async static Task<System.Drawing.Image> FromStreamToImage(Stream stream)
		{
			byte[] bytes;

			using (var memoryStream = new MemoryStream())
			{
				await stream.CopyToAsync(memoryStream);
				bytes = memoryStream.ToArray();
			}
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				return System.Drawing.Image.FromStream(ms);
			}
		}
		public static string FromImageToImageSource(System.Drawing.Image image)
		{
			byte[] bytes = (byte[])(new ImageConverter()).ConvertTo(image, typeof(byte[]));

			string base64 = Convert.ToBase64String(bytes);
			return string.Format("data:image/jpg;base64, {0}", base64);
		}
	}
}
