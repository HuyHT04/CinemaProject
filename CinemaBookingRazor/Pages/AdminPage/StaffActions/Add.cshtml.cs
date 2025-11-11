using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using BLL.Common;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.StaffActions
{
    public class AddModel : PageModel
    {
        private readonly IUserService _userService;

        public AddModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public AspNetUser Staff { get; set; } = new AspNetUser();

        // Property tạm để nhận mật khẩu từ form
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid input.";
                return Page();
            }

            // Kiểm tra Id trống
            if (string.IsNullOrWhiteSpace(Staff.Id))
            {
                ErrorMessage = "ID cannot be empty.";
                return Page();
            }

            // Kiểm tra Id đã tồn tại
            var existingById = await _userService.GetByIdStringAsync(Staff.Id);
            if (existingById.Success)
            {
                ErrorMessage = "ID already exists. Please choose another.";
                return Page();
            }

            // Kiểm tra Email đã tồn tại
            var existingByEmail = await _userService.GetByEmailAsync(Staff.Email);
            if (existingByEmail.Success)
            {
                ErrorMessage = "Email already exists. Please use another email.";
                return Page();
            }

            // Lưu mật khẩu
            Staff.PasswordHash = Password;

            // Thêm staff
            var response = await _userService.CreateAsync(Staff);
            if (response.Success)
            {
                return Redirect("/AdminPage/ManageStaff/ManageStaff");
            }
            else
            {
                ErrorMessage = response.Message;
                return Page();
            }
        }

    }
}
