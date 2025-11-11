using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using BLL.Common;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.StaffActions
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public AspNetUser Staff { get; set; } = new AspNetUser();

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string id)
        {

            var response = await _userService.GetByIdStringAsync(id);

            if (response.Success && response.Data != null)
            {
                Staff = response.Data;
                return Page();
            }
            else
            {
                return Redirect("/AdminPage/ManageStaff");
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid input.";
                return Page();
            }

            // Kiểm tra email có bị trùng với user khác không
            var existingByEmail = await _userService.GetByEmailAsync(Staff.Email);
            if (existingByEmail.Success && existingByEmail.Data.Id != Staff.Id)
            {
                ErrorMessage = "Email already exists. Please use another email.";
                return Page();
            }

            // Update staff
            var response = await _userService.UpdateAsync(Staff);

            if (response.Success)
                return Redirect("/AdminPage/ManageStaff/ManageStaff");
            else
            {
                ErrorMessage = response.Message;
                return Page();
            }
        }

    }
}
