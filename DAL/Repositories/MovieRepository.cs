using DAL.Data;
using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaBookingRazorContext context) : base(context) { }

        public async Task<Movie?> GetByNameAsync(string title)
        {
            return await _context.Movies
                                 .FirstOrDefaultAsync(m => m.Title.ToLower() == title.ToLower());
        }
        public async Task<bool> ExistsByTitleAsync(string title, int excludeId = 0)
        {
            return await Task.FromResult(_context.Movies
                .Any(m => m.Title.ToLower() == title.ToLower() && m.Id != excludeId));
        }
    }
}
