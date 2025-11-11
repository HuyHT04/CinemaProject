using BLL.Interfaces;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaBookingRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IEnumerable<Movie> Movies { get; set; } = Enumerable.Empty<Movie>();

        public async Task OnGetAsync()
        {
            var resp = await _movieService.GetAllAsync();
            if (resp != null && resp.Success && resp.Data != null)
            {
                Movies = resp.Data;
            }
            else
            {
                Movies = Enumerable.Empty<Movie>();
            }
        }

        // Logout handler (form uses asp-page-handler="Logout")
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}