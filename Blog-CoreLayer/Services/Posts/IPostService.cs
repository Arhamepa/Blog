using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.DTOs.Post;
using CodeYad_Blog.CoreLayer.Utilities;

namespace Blog_CoreLayer.Services.Posts;

public interface IPostService
{
    OperationResult CreatePost(CreatePostDto command);
    OperationResult EditPost(EditPostDto command);
    PostDto GetPostById(int id);
    PostDto GetPostBySlug(string slug);
    PostFilterDto GetPostFilterByP(PostFilterParams filterParams);
    bool IsSlugExist(string slug);
    List<PostDto> GetRelatedPost(int categoryId);
    List<PostDto> GetPopularPost();
    void IncreaseVisit(int postId);



}