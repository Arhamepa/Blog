using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    public class SearchModel : PageModel
    {
       private readonly IPostService _postService;
       public PostFilterDto Filter { get; set; }
       public SearchModel(IPostService postService)
       {
           this._postService = postService;
       }

       public void OnGet(int pageId = 1, string categorySlug = null, string q = null)
       {
           Filter = _postService.GetPostFilterByP(new PostFilterParams()
           {
               PageId = pageId,
               CategorySlug = categorySlug,
               Take = 5,
               Title = q
           });
       }

       public IActionResult OnGetPagination(int pageId=1 , string categorySlug = null , string q = null)
       {
           var model = _postService.GetPostFilterByP(new PostFilterParams()
           {
               CategorySlug = categorySlug,
               PageId = pageId,
               Title = q,
               Take = 5,
           });
           return Partial("_SearchPartial", model);
       }
    }
}
