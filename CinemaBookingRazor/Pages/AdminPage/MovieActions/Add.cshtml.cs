using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using BLL.Common;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.MovieActions
{
    public class AddModel : PageModel
    {
        private readonly IMovieService _movieService;

        public AddModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [BindProperty]
        public Movie Movie { get; set; } = new Movie();

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid input.";
                return Page();
            }

            // Kiểm tra trùng tên trước khi thêm
            var existingMovie = await _movieService.GetByNameAsync(Movie.Title);
            if (existingMovie != null)
            {
                ErrorMessage = "Movie title already exists. Please choose a different title.";
                return Page();
            }

            // Gọi service để thêm movie
            var response = await _movieService.CreateAsync(Movie);

            if (response.Success)
            {
                // Thêm thành công, quay về trang quản lý movie
                return Redirect("/AdminPage/ManageMovie/ManageMovie");
            }
            else
            {
                ErrorMessage = response.Message;
                return Page();
            }
        }

    }
}
