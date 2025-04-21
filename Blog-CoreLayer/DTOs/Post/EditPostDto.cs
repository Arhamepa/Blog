using Blog_DataLayer.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.DTOs.Post;

public class EditPostDto
{
    public int PostId { get; set; }
    public int Category_Id { get; set; }

    public int? SubCategory_Id { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }
    public string Description { get; set; }
    public bool IsSpecial { get; set; }

    public IFormFile ImageName { get; set; }

    
}