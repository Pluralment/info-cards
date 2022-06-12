using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace card_client.extensions
{
    public static class OpenFileDialogExtension
    {
        public static async Task<BitmapImage> LoadBitmapImage(this OpenFileDialog fileDialog)
        {
            var image = new BitmapImage();
            await image.ImageToBitmap(fileDialog.FileName.ToString());
            return image;
        }
    }
}
