using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using BLL.Common;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.ManageMovie
{
    public class ViewModel : PageModel
    {
        private readonly IMovieService _movieService;

        public ViewModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie Movie { get; set; } = new Movie();
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _movieService.GetByIdAsync(id);
            if (response.Success && response.Data != null)
            {
                Movie = response.Data;
                return Page();
            }
            else
            {
                ErrorMessage = response.Message ?? "Movie not found";
                return Page();
            }
        }
    }
}
