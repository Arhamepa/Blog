using Blog_CoreLayer.DTOs.User;
using Blog_CoreLayer.Services.User;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{

    public class UserController : AdminBaseController
    {
        private readonly IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public IActionResult Index(int pageId = 1)
        {

            return View(_usersService.GetUserByFilter(pageId, 1));
        }
        [HttpPost]
        public IActionResult Edit(EditUserDto editUserDto)
        {
            var result = _usersService.EditUser(editUserDto);
            if (result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return RedirectToAction("Index");
            }
            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        public IActionResult ShowEditModal(int userId)
        {
            var user = _usersService.GetUserById(userId);
            return PartialView("_EditUser", new EditUserDto() { FullName = user.FullName, Role = user.Role });
        }
    }
}