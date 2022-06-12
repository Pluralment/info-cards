using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace card_client.extensions
{
    public static class BitmapImageExtension
    {
        private static void InitBitmap(BitmapImage image, Stream stream)
        {
            stream.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = stream;
            image.EndInit();
            //image.Freeze();
        }

        public static BitmapImage InitBitmapWithStream(this BitmapImage image, Stream stream)
        {
            InitBitmap(image, stream);
            return image;
        }

        public async static Task ImageToBitmap(this BitmapImage image, string imagePath)
        {
            using (var fileStream = File.Open(imagePath, FileMode.Open))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    InitBitmap(image, memoryStream);
                }
            }
        }

        public static void BytesToBitmap(this BitmapImage image, byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                InitBitmap(image, memoryStream);
            }
        }
    }
}
