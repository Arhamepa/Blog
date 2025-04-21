using Blog_CoreLayer.DTOs;
using Blog_CoreLayer.DTOs.User;
using Blog_CoreLayer.Utilities;
using Blog_DataLayer.Context;
using CodeYad_Blog.CoreLayer.Utilities;
using Blog_DataLayer.Entities;

namespace Blog_CoreLayer.Services.User
{

    public class UserService:IUsersService
    {
        private readonly BlogDbContext _context;

        public UserService(BlogDbContext blogDbContext)
        {
            _context = blogDbContext;
        }

        public OperationResult EditUser(EditUserDto command)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == command.UserId);
            if (user == null)
                return OperationResult.NotFound();

            user.FullName = command.FullName;
            user.Role = command.Role;
            _context.SaveChanges();
            return OperationResult.Success();
        }

        public OperationResult RegisterUser(UserRegisterDto userDto)
        {
            var IsUserNameExist = _context.Users.Any((it => it.UserName == userDto.UserName));

            if (IsUserNameExist)
                return OperationResult.Error("نام کاربر تکراری می باشد.");

            var PasswordHash = userDto.Password.EncodeToMd5();
            _context.Users.Add(new Blog_DataLayer.Entities.User()
            {
                UserName = userDto.UserName,
                FullName = userDto.FullName,
                IsDelete = false,
                Password = PasswordHash,
                CreateDate = DateTime.Now,
                Role = Blog_DataLayer.Entities.User.UserRole.User
            }
            );

        _context.SaveChanges();
            return OperationResult.Success("عملیات با موفقیت انجام شد.");
        }

        public UserDto LoginUser(LoginUserDto loginUser)
        { 
            var passwordHash = loginUser.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault((it => it.UserName == loginUser.UserName && it.Password == passwordHash));
            if (user == null)
                return null;

            var userDto =  new UserDto()
            {
                UserName = user.UserName,
                UserId = user.Id ,
                Password = user.Password,
                FullName = user.FullName,
                RegisterDate = user.CreateDate,
                Role = user.Role
            };
            return userDto;
        }
        public UserFilterDto GetUserByFilter(int pageId, int take)
        {
            var users = _context.Users.OrderByDescending(d => d.Id)
                .Where(c => !c.IsDelete);

            var skip = (pageId - 1) * take;
            var model = new UserFilterDto()
            {
                Users = users.Skip(skip).Take(take).Select(user => new UserDto()
                {
                    FullName = user.FullName,
                    Password = user.Password,
                    Role = user.Role,
                    UserName = user.UserName,
                    RegisterDate = user.CreateDate,
                    UserId = user.Id
                }).ToList()
            };
            model.GeneratePaging(users,take,pageId);
            return model;
        }

        public UserDto GetUserById(int userId)
        {
              var user =  _context.Users.FirstOrDefault(it => it.Id == userId);
              return new UserDto()
              {
                  UserName = user.UserName,
                  FullName = user.FullName,
                  Role = user.Role
              };
        }
    }
}
