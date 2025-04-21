using Blog.Areas.Admin.Models.Posts;
using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Services.Posts;
using Blog_CoreLayer.Utilities;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
   
    public class PostController : AdminBaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int pageId =1 , string title="",string categorySlug ="")
        {
            var param = new PostFilterParams()
            {
                CategorySlug = categorySlug,
                Title = title,
                PageId = pageId,
                Take = 3
            };
            var model = _postService.GetPostFilterByP(param);
            return View(model);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.CreatePost(new CreatePostDto()
            {
                Title = model.Title,
                Slug = model.Slug,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId != 0 ? model.SubCategoryId : null,
                Description = model.Description,
                ImageFile = model.ImageFile,
                IsSpecial = model.IsSpecial,
                UserId = User.GetUserId()
            });
           
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }


        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
                RedirectToAction("Index");
            var model = new EditPostViewModel()
            {
                Title = post.Title,
                Slug = post.Slug,
                SubCategoryId = post.SubCategory_Id,
                CategoryId = post.Category_Id,
                Description = post.Description,
                IsSpecial = post.IsSpecial
              
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id , EditPostViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = _postService.EditPost(new EditPostDto()
            {
                Title = model.Title,
                Slug = model.Slug,
                Category_Id = model.CategoryId,
                SubCategory_Id = model.SubCategoryId == 0 ? null : model.SubCategoryId,
                Description = model.Description,
                ImageName = model.ImageFile ,
                PostId = id,
                IsSpecial = model.IsSpecial
                
               
            });
            if (result.Status != OperationResultStatus.Success)
            {
                ModelState.AddModelError(nameof(CreatePostViewModel.Slug), result.Message);
                return View(model);
            }
          

            return RedirectToAction("Index");
        }




    }
}
