﻿using Blog_CoreLayer.DTOs.Post;

namespace Blog_CoreLayer.DTOs;

public class MainPageDto
{
    public List<PostDto> LatestPosts { get; set; }
    public List<PostDto> SpecialPosts { get; set; }
    public List<MainPageCategoryDto> Categories { get; set; }

    public class MainPageCategoryDto
    {
        public bool IsMainCategory { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int ChildCount { get; set; }
    }
}   