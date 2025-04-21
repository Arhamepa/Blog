using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Services;
using Blog_CoreLayer.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
       
        private readonly IPostService _postService;
        private readonly IMainPageService _mainPageService;
        public IndexModel(IPostService postService, IMainPageService mainPageService)
        {
           
            _postService = postService;
            _mainPageService = mainPageService;
        }

        public MainPageDto MainPage { get; set; }

        public void OnGet()
        {
            MainPage = _mainPageService.GetData();
        }
        public IActionResult OnGetLatestPosts(string categorySlug)
        {
            var model = _postService.GetPostFilterByP(new PostFilterParams()
            {
                CategorySlug = categorySlug,
                PageId = 1,
                Take = 6,
            });
            return Partial("_LatestPosts", model.Posts);
        }

        public IActionResult OnGetPopularPost()
        {
            return Partial("_PopularPost", _postService.GetPopularPost());
        }
    }
}