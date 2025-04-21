using System.ComponentModel.DataAnnotations;

namespace Blog.Areas.Admin.Models.Posts;

public class CreatePostViewModel
{

    [Display(Name = "انتخاب دسته بندری")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    public int CategoryId { get; set; }

    [Display(Name = "انتخاب دسته بندری")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    public int? SubCategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    public string Title { get; set; }
    [Display(Name = "Slug")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    public string Slug { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    [UIHint("CkEditor4")]
    public string Description { get; set; }

    [Display(Name = "عکس")]
    [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
    public IFormFile ImageFile { get; set; }

    [Display(Name = "پست ویژه")]
    public bool IsSpecial { get; set; }

}