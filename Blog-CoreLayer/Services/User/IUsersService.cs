using Blog_CoreLayer.DTOs.User;
using CodeYad_Blog.CoreLayer.Utilities;

namespace Blog_CoreLayer.Services.User
{
    public interface IUsersService
    {
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto userDto);
        UserDto LoginUser(LoginUserDto loginUser);
        UserFilterDto GetUserByFilter(int pageId, int take);
        UserDto GetUserById(int id);
    }
}
