namespace Blog_CoreLayer.Utilities;

public class Directories
{
    public const string PostImageDirectory = "wwwroot/images/posts";
    public const string UploadPostImageDirectory = "wwwroot/images/posts/upload";
    public static string PostImageFolder(string imageName) => $"{PostImageDirectory.Replace("wwwroot", "")}/{imageName}";
    public static string UploadPostImageFolder(string imageName) => $"{UploadPostImageDirectory.Replace("wwwroot", "")}/{imageName}";
}