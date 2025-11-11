using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using BLL.Common; // chứa ServiceResponse
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.ManageStaff
{
    public class ManageStaffModel : PageModel
    {
        private readonly IUserService _userService;

        public ManageStaffModel(IUserService userService)
        {
            _userService = userService;
        }

        // Danh sách nhân viên để hiển thị trong Razor Page
        public List<AspNetUser> StaffList { get; set; } = new List<AspNetUser>();

        // Thông báo lỗi nếu có
        public string ErrorMessage { get; set; } = string.Empty;

        // Load dữ liệu khi mở trang
        public async Task OnGetAsync()
        {
            // Gọi service lấy tất cả user
            ServiceResponse<IEnumerable<AspNetUser>> response = await _userService.GetAllAsync();

            if (response.Success && response.Data != null)
            {
                // Chỉ lấy các user có RoleName = "Staff"
                StaffList = response.Data
                    .Where(u => u.RoleName == "Staff" || u.RoleName == "Admin" || u.RoleName == "Customer")
                    .ToList();
            }
            else
            {
                ErrorMessage = response.Message ?? "Failed to load staff data.";
            }
        }
    }
}
