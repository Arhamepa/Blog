using Blog_CoreLayer.DTOs.Comment;
using Blog_DataLayer.Context;
using Blog_DataLayer.Entities;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blog_CoreLayer.Services.Comments;

public class CommentService : ICommentService
{
    private readonly BlogDbContext _context;

    public CommentService(BlogDbContext context)
    {
        _context = context;
    }


    public OperationResult CreateComment(CreateCommentDto command)
    {
        var comment = new PostComment()
        {
            Text = command.Text,
            User_Id = command.User_Id,
            Post_Id = command.Post_Id
        };
        _context.PostComments.Add(comment);
        _context.SaveChanges();
        return OperationResult.Success();
    }

    public List<CommentDto> GetPostComments(int postId)
    {
        return  _context.PostComments
            .Include(it => it.User)
            .Where(c => c.Post_Id == postId)
            .Select(comment => new CommentDto()
            {
                Text = comment.Text,
                UserFullName = comment.User.FullName,
                Post_Id = comment.Post_Id,
                CommentId = comment.Id,
                CreateDate = comment.CreateDate

            })
            .ToList();
    }
}