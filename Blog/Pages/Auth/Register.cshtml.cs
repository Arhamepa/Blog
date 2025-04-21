using System.ComponentModel.DataAnnotations;
using Blog_CoreLayer.DTOs.User;
using Blog_CoreLayer.Services.User;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly IUsersService _usersService;

        #region Properties

        [Display(Name = "نام کاربری")]
        [Required (ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; }
        [Display(Name = " کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }

        #endregion

        public RegisterModel(IUsersService userService)
        {
            _usersService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var result = _usersService.RegisterUser(new UserRegisterDto()
            {
                UserName = UserName,
                FullName = FullName,
                Password = Password
            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName" , result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
