namespace Blog_CoreLayer.DTOs.Comment;

public class CommentDto
{
    public string Text { get; set; }
    public int Post_Id { get; set; }
    public string UserFullName { get; set; }
    public int CommentId { get; set; }
    public DateTime CreateDate { get; set; }
}