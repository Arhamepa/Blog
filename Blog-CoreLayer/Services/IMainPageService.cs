using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.Mappers;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services;

public interface IMainPageService
{
    MainPageDto GetData();
}

public class MainPageService:IMainPageService
{
    private readonly BlogDbContext _context;

    public MainPageService(BlogDbContext context)
    {
        _context = context;
    }

    public MainPageDto GetData()
    {
        var categories = _context.Categories
            .OrderByDescending(d => d.Id)
            .Take(6)
            .Include(c => c.Posts)
            .Include(c => c.SubCatPosts)
            .Select(category => new MainPageDto.MainPageCategoryDto()
            {
                Title = category.Title,
                Slug = category.Slug,
                ChildCount = category.Posts.Count + category.SubCatPosts.Count,
                IsMainCategory = category.ParentId == null
            }).ToList();

        var latestPosts = _context.Posts
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .OrderByDescending(d => d.Id)
            .Take(6).Select(latestPost => PostMapper.MapToDto(latestPost)).ToList();

        var specialPosts = _context.Posts
            .OrderByDescending(d => d.Id)
            .Include(c => c.Category)
            .Include(c => c.SubCategory)
            .Where(it => it.IsSpecial)
            .Take(4)
            .Select(post => PostMapper.MapToDto(post)).ToList();



        return new MainPageDto
        {
            Categories = categories,
            LatestPosts = latestPosts,
            SpecialPosts = specialPosts
        };

    }
}