using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.MovieActions
{
    public class DeleteModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DeleteModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        public string ErrorMessage { get; set; } = string.Empty;

        // Lấy dữ liệu Movie theo ID để hiển thị thông tin xác nhận
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _movieService.GetByIdAsync(id);
            if (!response.Success || response.Data == null)
            {
                ErrorMessage = response.Message;
                return Page();
            }

            Movie = response.Data;
            return Page();
        }

        // Xử lý xóa movie
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var deleteResponse = await _movieService.DeleteAsync(id);

            if (!deleteResponse.Success)
            {
                ErrorMessage = deleteResponse.Message;
                return Page();
            }

            // Thành công, quay về trang quản lý movie
            return Redirect("/AdminPage/ManageMovie/ManageMovie");
        }
    }
}
