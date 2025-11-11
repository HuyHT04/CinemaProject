using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMovieService : IGenericService<Movie> {
        Task<Movie?> GetByNameAsync(string title);
        Task<bool> ExistsByTitleAsync(string title, int excludeId = 0);
    }
}
