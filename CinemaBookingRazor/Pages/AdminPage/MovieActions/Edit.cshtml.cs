using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.MovieActions
{
    public class EditModel : PageModel
    {
        private readonly IMovieService _movieService;

        public EditModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        public string ErrorMessage { get; set; } = string.Empty;

        // Lấy dữ liệu Movie theo ID
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

        // Xử lý submit form
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng điền đầy đủ thông tin hợp lệ.";
                return Page();
            }

            // Kiểm tra tên phim đã tồn tại chưa (ngoại trừ chính phim đang edit)
            var exists = await _movieService.ExistsByTitleAsync(Movie.Title, Movie.Id);
            if (exists)
            {
                ErrorMessage = "Tên phim này đã tồn tại, vui lòng chọn tên khác.";
                return Page();
            }

            // Update movie
            var updateResponse = await _movieService.UpdateAsync(Movie);
            if (!updateResponse.Success)
            {
                ErrorMessage = updateResponse.Message;
                return Page();
            }

            // Thành công, quay về trang quản lý movie
            return Redirect("/AdminPage/ManageMovie/ManageMovie");
        }
    }
}
