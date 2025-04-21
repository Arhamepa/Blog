using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.Services.FileManager;

public interface IFileManager
{
    string SaveImageAndReturnImageName(IFormFile file, string savePath);
    string SaveFile(IFormFile file , string savePath);

   public void DeleteImageFile(string imageName, string imagePath);
}