using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Blog_CoreLayer.DTOs.User;
using Blog_CoreLayer.Services.User;
using CodeYad_Blog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly IUsersService _usersService;

        public LoginModel(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = " کلمه عبور را وارد کنید")]
        [MinLength(6 , ErrorMessage = "کلمه عبور باید بیشتر از 6 کاراکتر باشد")]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid ==false)
            {
                return Page();
            }

            var user = _usersService.LoginUser(new LoginUserDto()
            {
                UserName = UserName,
                Password = Password
            });

            if (user == null)
            {
                ModelState.AddModelError("UserName","کاربری با مشخصات وارد شده پیدا نشد!");
                return Page();
            } 
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Test","Test"),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("../Index");

        }
    }
}
