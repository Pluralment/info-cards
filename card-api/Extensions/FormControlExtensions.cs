using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace card_api.Extensions
{
    public static class FormControlExtensions
    {
        public async static Task<byte[]> ToBytesArray(this IFormFile formFile)
        {
            byte[] bytes = null;
            if (formFile != null)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }
            return bytes;
        }
    }
}
