﻿namespace Blog_CoreLayer.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string MetaTag { get; set; }
    public string MetaDescription { get; set; }
    public int? ParentId { get; set; }
}