using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.Linq;

namespace WebApplication1
{
    public class KonvertovanjeSlike
    {
        public static string GetImageBase64(byte[] plakat)
        {
            return plakat != null ? $"data:image/jpg;base64,{Convert.ToBase64String(plakat)}" : null;
        }

        public static byte[] GetImageByteArray(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var fs = file.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }   
    }
}
