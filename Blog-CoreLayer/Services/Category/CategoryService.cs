using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.Mappers;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using CodeYad_Blog.CoreLayer.Utilities;
using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.Services.Category;
public class CategoryService :ICategoryService
{
    private readonly BlogDbContext _context;

    public CategoryService(BlogDbContext context)
    {
        _context = context;
    }

    public OperationResult CreateCategory(CreateCategoryDto categoryDto)
    {
        if(IsSlugExist(categoryDto.Slug))
            return OperationResult.Error("Slug تکراری می باشد");

        var category = new Blog_DataLayer.Entities.Category()
        {
            Title = categoryDto.Title,
            Slug = categoryDto.Slug.ToSlug(),
            MetaTag = categoryDto.MetaTag,
            MetaDescription = categoryDto.MetaDescription,
            ParentId = categoryDto.ParentId
        };
        _context.Add(category);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public OperationResult CategoryEdit(EditCategoryDto command)
    {
        var category = _context.Categories.FirstOrDefault(it => it.Id == command.Id);
        if (category == null)
            OperationResult.NotFound();
        
        if (command.Slug.ToSlug() != category.Slug)
        {
            if (IsSlugExist(command.Slug))
            {
                return OperationResult.Error("Slug تکراری می باشد");
            }
        }

        category.Title = command.Title;
        category.Slug = command.Slug.ToSlug();
        category.MetaTag = command.MetaTag;
        category.MetaDescription = command.MetaDescription;
      

        _context.SaveChanges();
        return OperationResult.Success();

    }

    public List<CategoryDto> GetAllCategories()
    {
        return _context.Categories.Select(its => CategoryMapper.Map(its)).ToList();
    }

    public List<CategoryDto> GetSubCategories(int parentId)
    {
       return  _context.Categories.Where(it=>it.ParentId == parentId).Select(its => CategoryMapper.Map(its)).ToList();
    }

    public CategoryDto CategoryGetBy(int id)
    {
        var category = _context.Categories.FirstOrDefault(it => it.Id == id);
        if(category == null)
            return null;
        return CategoryMapper.Map(category);
    }

    public CategoryDto CategoryGetBy(string slug)
    {
        var category = _context.Categories.FirstOrDefault(it => it.Slug == slug);
        if (category == null)
            return null;
        return CategoryMapper.Map(category);
    }

    public bool IsSlugExist(string slug)
    {

        return _context.Categories.Any(it => it.Slug == slug.ToSlug());
    }
}