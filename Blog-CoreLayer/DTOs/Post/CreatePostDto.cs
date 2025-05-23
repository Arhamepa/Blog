﻿using Microsoft.AspNetCore.Http;

namespace Blog_CoreLayer.DTOs.Post;

public class CreatePostDto
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public bool IsSpecial { get; set; }


}