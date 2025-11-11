using BLL.Interfaces;
using BLL.Common;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CinemaBookingRazor.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var exists = await _userService.GetByEmailAsync(Input.Email);
            if (exists.Success && exists.Data != null)
            {
                ModelState.AddModelError("Input.Email", "Email already registered.");
                return Page();
            }

            var newUser = new AspNetUser
            {
                Id = Guid.NewGuid().ToString(),
                FullName = Input.FullName,
                Email = Input.Email,
                PasswordHash = Input.Password, 
                RoleName = "Customer"
            };

            var createResp = await _userService.CreateAsync(newUser);
            if (!createResp.Success)
            {
                ModelState.AddModelError(string.Empty, createResp.Message ?? "Register failed.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.Id),
                new Claim(ClaimTypes.Name, newUser.FullName ?? newUser.Email),
                new Claim(ClaimTypes.Email, newUser.Email),
                new Claim(ClaimTypes.Role, newUser.RoleName)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

            return LocalRedirect("/");
        }
    }

    public class RegisterInput
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
