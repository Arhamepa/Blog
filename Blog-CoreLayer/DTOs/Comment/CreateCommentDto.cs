namespace Blog_CoreLayer.DTOs.Comment;

public class CreateCommentDto
{
    public string Text { get; set; }
    public int Post_Id { get; set; }
    public int User_Id { get; set; }
}