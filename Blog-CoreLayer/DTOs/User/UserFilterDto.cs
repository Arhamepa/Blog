using Blog_CoreLayer.Utilities;

namespace Blog_CoreLayer.DTOs.User;

public class UserFilterDto:BasePagination
{
    public List<UserDto> Users { get; set; }
}