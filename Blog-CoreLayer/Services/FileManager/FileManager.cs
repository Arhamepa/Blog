using Blog_CoreLayer.Utilities;
using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.Services.FileManager;

public class FileManager : IFileManager
{
    public string SaveImageAndReturnImageName(IFormFile file, string savePath)
    {
        var isNotImage = !ImageValidation.Validator(file);
        if (isNotImage)
            throw new Exception();
        return SaveFile(file, savePath);
    }


    public string SaveFile(IFormFile file, string savePath)
    {
        if (file == null)
        {
            throw new Exception("File is Null");
        }

        var filename = $"{Guid.NewGuid()}{file.FileName}";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), savePath.Replace("/", "\\"));
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var fullPath= Path.Combine(folderPath, filename);
        using var stream = new FileStream(fullPath, FileMode.Create);
        file.CopyTo(stream);
        return filename;
    }

    public void DeleteImageFile(string imageName, string imagePath)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), imagePath, imageName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}