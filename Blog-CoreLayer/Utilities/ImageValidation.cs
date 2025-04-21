using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.Utilities
{
    public class ImageValidation
    {
        public static bool Validator(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            if (extension == null)
            {
                return false;
            }

            return extension.ToLower() == ".png" || extension.ToLower() == ".jpg";
        }

        public static bool Validator(IFormFile file)
        {
            try
            {
                using var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
                
            }
        }
    }
}
