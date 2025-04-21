using Blog_CoreLayer.DTOs.Comment;
using CodeYad_Blog.CoreLayer.Utilities;

namespace Blog_CoreLayer.Services.Comments;

public interface ICommentService
{
    OperationResult CreateComment(CreateCommentDto command);
    List<CommentDto> GetPostComments(int postId);
}