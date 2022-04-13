using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Borna.Common.Security
{
   public static class ImageValidator
    {
        public static bool IsImage(this IFormFile image) {
            try
            {
                var img = System.Drawing.Image.FromStream(image.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
