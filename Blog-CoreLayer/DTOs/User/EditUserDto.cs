namespace Blog_CoreLayer.DTOs.User;

public class EditUserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public Blog_DataLayer.Entities.User.UserRole Role { get; set; }
}