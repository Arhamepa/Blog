using Blog_CoreLayer.Services.FileManager;
using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        [Route("/Upload/Article")]
        public IActionResult UploadPostImages(IFormFile upload)
        {
            if (upload == null)
            {
                return BadRequest();
            }


            var imageName = _fileManager.SaveFile(upload, Directories.UploadPostImageDirectory);

            return Json(new { Uploaded = true, url = Directories.UploadPostImageFolder(imageName)});
        }
    }
    
}
