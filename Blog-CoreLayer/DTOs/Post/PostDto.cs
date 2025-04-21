using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.DTOs.Post;

public class PostDto
{
    public int PostId { get; set; }
    public string UserFullName { get; set; }
    public int Category_Id { get; set; }
    public int Visit { get; set; }
    public string Title { get; set; }
    public int? SubCategory_Id { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsSpecial { get; set; }
    public CategoryDto SubCategory { get; set; }
    public CategoryDto Category { get; set; }

}