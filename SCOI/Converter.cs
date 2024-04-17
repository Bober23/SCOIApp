using MudBlazor;
using MudBlazor.Charts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
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
            using var ms = new MemoryStream();
            var bitmap = new Bitmap(image);
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = ms.ToArray();

            string base64 = Convert.ToBase64String(bytes);
            return string.Format("data:image/jpg;base64, {0}", base64);
        }
        public static byte[] FromBitmapToByte(Bitmap input, int targetWidth = 0, int targetHeight = 0)
        {
            int height = input.Height;
            int width = input.Width;
            if (targetHeight != 0)
            {
                height = targetHeight;
            }
            if (targetWidth != 0)
            {
                width = targetWidth;
            }
            input = new Bitmap(input, width, height);
            //создаем временное изображние с нужным нам форматом хранения.
            //так как обработка побайтовая, там важно расположение байтов в картинке.
            //а оно опеределено форматом хранения

            byte[] input_bytes = new byte[0]; //пустой массивчик байт

            //по этому создадим новый битмап с нужным нам 3х байтовым форматом.
            using (Bitmap _tmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                //устанавливаем DPI такой же как у исходного
                _tmp.SetResolution(input.HorizontalResolution, input.VerticalResolution);

                //рисуем исходное изображение на временном, "типо-копируем"
                using (var g = Graphics.FromImage(_tmp))
                {
                    g.DrawImageUnscaled(input, 0, 0);
                }
                return getImgBytes(_tmp); //получаем байты изображения, см. описание ф-ции ниже
            }

        }

        public static Bitmap FromByteToBitmap(byte[] bytes, int width, int height, float hRes, float vRes)
        {
            Bitmap img_ret = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            img_ret.SetResolution(hRes, vRes);

            writeImageBytes(img_ret, bytes);

            return img_ret;
        }
        static byte[] getImgBytes(Bitmap img)
        {
            byte[] bytes = new byte[img.Width * img.Height * 3];  //выделяем память под массив байтов
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.ReadOnly,
                img.PixelFormat);
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);  //копируем байты изображения в массив
            img.UnlockBits(data);   //разблокируем изображение
            return bytes; //возвращаем байты
        }

        static void writeImageBytes(Bitmap img, byte[] bytes)
        {
            var data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),  //блокируем участок памати, занимаемый изображением
                ImageLockMode.WriteOnly,
                img.PixelFormat);
            Marshal.Copy(bytes, 0, data.Scan0, bytes.Length); //копируем байты массива в изображение

            img.UnlockBits(data);  //разблокируем изображение
        }
    }
}
