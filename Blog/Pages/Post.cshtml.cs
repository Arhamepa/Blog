using System.ComponentModel.DataAnnotations;
using Blog_CoreLayer.DTOs.Comment;
using Blog_CoreLayer.DTOs.Post;
using Blog_CoreLayer.Services.Comments;
using Blog_CoreLayer.Services.Posts;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages
{
    
    public class PostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostModel(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public PostDto Post { get; set; }
        [BindProperty]
        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0}  را وارد کنید")]
        public string Text { get; set; }
        [BindProperty]
        public int PostId { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<PostDto> RelatedPost { get; set; }


        public IActionResult OnGet(string slug)
        {
          Post = _postService.GetPostBySlug(slug);

            if (Post == null)
            {
                return NotFound();
            } 
            RelatedPost = _postService.GetRelatedPost(Post.SubCategory_Id ?? Post.Category_Id);
            Comments= _commentService.GetPostComments(Post.PostId);
            _postService.IncreaseVisit(Post.PostId);
            return Page();
        }

        public IActionResult OnPost(string slug)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToPage("Post" , new {slug});
            }

            if (!ModelState.IsValid)
            {

                Post = _postService.GetPostBySlug(slug);
                RelatedPost = _postService.GetRelatedPost(Post.SubCategory_Id ?? Post.Category_Id);
                Comments = _commentService.GetPostComments(Post.PostId);
                return Page();
            }

            _commentService.CreateComment(new CreateCommentDto()
            {
                Post_Id = PostId,
                User_Id = User.GetUserId(),
                Text = Text
            });
            return  RedirectToPage("Post", new { slug });
        }
    }
}
