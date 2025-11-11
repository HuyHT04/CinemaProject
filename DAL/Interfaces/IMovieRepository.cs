using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie?> GetByNameAsync(string title);
        Task<bool> ExistsByTitleAsync(string title, int excludeId = 0);

    }
}
