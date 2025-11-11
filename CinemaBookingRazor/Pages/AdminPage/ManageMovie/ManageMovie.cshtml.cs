using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Model;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages.AdminPage.ManageMovie
{
    public class ManageMovieModel : PageModel
    {
        private readonly IMovieService _movieService;

        public ManageMovieModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public List<Movie> MovieList { get; set; } = new List<Movie>();

        public string ErrorMessage { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            var response = await _movieService.GetAllAsync();
            if (response.Success && response.Data != null)
            {
                // Chuyển IEnumerable<Movie> sang List<Movie>
                MovieList = response.Data.ToList();
            }
            else
            {
                ErrorMessage = response.Message;
            }
        }
    }
}
