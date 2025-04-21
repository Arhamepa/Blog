using Blog_CoreLayer.DTOs;
using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.Mappers;

public class CategoryMapper
{
    public static CategoryDto Map(Category category)
    {

        return new CategoryDto()
        {
            Id = category.Id,
            Title = category.Title,
            Slug = category.Slug,
            MetaDescription = category.MetaDescription,
            MetaTag = category.MetaTag,
            ParentId = category.ParentId

        };
    }

}