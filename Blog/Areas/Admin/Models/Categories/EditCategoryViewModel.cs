using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.Models.Categories;

public class EditCategoryViewModel
{
    public int Id { get; set; }
    [Display(Name = "انتخاب دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int CategoryId { get; set; }

    [Display(Name = "انتخاب زیر دسته بندی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int? SubCategoryId { get; set; }
    [Display(Name = "عنوان")]
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
}