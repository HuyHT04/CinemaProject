using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.StaffActions
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public AspNetUser Staff { get; set; } = new AspNetUser();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var response = await _userService.GetByIdStringAsync(id); // dùng string Id
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
            if (string.IsNullOrEmpty(Staff.Id))
                return Redirect("/AdminPage/ManageStaff");

            var response = await _userService.DeleteStringAsync(Staff.Id); // xóa theo string Id

            return Redirect("/AdminPage/ManageStaff/ManageStaff");
        }
    }
}
