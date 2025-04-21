using Blog_CoreLayer.DTOs.Post;
using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.Mappers;

public class PostMapper
{
    public static Post MapCreatePostDtoToPost(CreatePostDto dto)
    {
        return new Post()
        {
            Title = dto.Title,
            User_Id = dto.UserId,
            Category_Id = dto.CategoryId,
            Description = dto.Description,
            Visit = 0,
            IsDelete = false,
            Slug = dto.Slug,
            SubCategory_Id = dto.SubCategoryId,
            IsSpecial = dto.IsSpecial
        };
    }
    public static PostDto MapToDto(Post post)
    {
        return new PostDto()
        {
            Title = post.Title,
            UserFullName = post.User?.FullName,
            Category_Id = post.Category_Id,
            Description = post.Description,
            Visit = post.Visit,
            CreateDate = post.CreateDate,
            Slug = post.Slug,
            Category = post.Category == null ? null : CategoryMapper.Map(post.Category),
            ImageName = post.ImageName,
            PostId = post.Id,
            SubCategory = post.SubCategory == null? null : CategoryMapper.Map(post.SubCategory),
            SubCategory_Id = post.SubCategory_Id,
            IsSpecial = post.IsSpecial
        };
    }

    public static Post EditPostByMapper(EditPostDto dto, Post post)
    {
        post.Title = dto.Title;
        post.Category_Id = dto.Category_Id;
        post.Description = dto.Description;
        post.Slug=dto.Slug;
        post.SubCategory_Id = dto.SubCategory_Id;
        post.IsSpecial=dto.IsSpecial;
        
        return post;
        
    }
}