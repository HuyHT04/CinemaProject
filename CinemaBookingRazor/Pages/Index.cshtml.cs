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
        private const int PageSize = 9;   // số phim / trang

        public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task OnGetAsync(int page = 1)
        {
            CurrentPage = page < 1 ? 1 : page;

            // lấy toàn bộ phim từ service
            var resp = await _movieService.GetAllAsync();
            var list = (resp?.Success == true && resp.Data != null)
                ? resp.Data.ToList()
                : new List<Movie>();

            int total = list.Count;
            TotalPages = (int)Math.Ceiling(total / (double)PageSize);

            Movies = list
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}