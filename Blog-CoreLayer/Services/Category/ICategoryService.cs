using Blog_CoreLayer.DTOs;
using CodeYad_Blog.CoreLayer.Utilities;

namespace Blog_CoreLayer.Services.Category;

public interface ICategoryService
{
    OperationResult CreateCategory(CreateCategoryDto command);
    OperationResult CategoryEdit(EditCategoryDto command);
    List<CategoryDto> GetAllCategories();
    List<CategoryDto> GetSubCategories(int parentId);
    CategoryDto CategoryGetBy(int id);
    CategoryDto CategoryGetBy(string slug);
    bool IsSlugExist(string slug);
}