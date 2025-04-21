using System.ComponentModel.DataAnnotations;
using Blog_CoreLayer.DTOs;

namespace Blog.Areas.Admin.Models.Categories;

public class CreateCategoryViewModel
{
    [Display(Name ="عنوان")]
    [Required(ErrorMessage = "وارد کردن  {0} اجباری می باشد.")]
    public string Title { get; set; }
    [Display(Name = "Slug")]
    [Required(ErrorMessage = "وارد کردن  {0} اجباری می باشد.")]
    public string Slug { get; set; }
    [Display(Name = "MetaTag را با - از هم جدا کنید")]
    public string MetaTag { get; set; }
    [DataType(DataType.MultilineText)]
    public string MetaDescription { get; set; }
    public int? ParentId { get; set; }

    public  CreateCategoryDto MapToDto()
    {
        var category = new CreateCategoryDto()
        {
            Title = Title,
            Slug = Slug,
            MetaTag = MetaTag,
            MetaDescription = MetaDescription,
            ParentId = ParentId,

        };
        return category;
    }

}