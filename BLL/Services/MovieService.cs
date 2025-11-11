using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MovieService : GenericService<Movie>, IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository repo) : base(repo)
        {
            _movieRepository = repo;
        }

        public async Task<Movie?> GetByNameAsync(string title)
        {
            return await _movieRepository.GetByNameAsync(title);
        }
        public async Task<bool> ExistsByTitleAsync(string title, int excludeId = 0)
        {
            return await _movieRepository.ExistsByTitleAsync(title, excludeId);
        }
    }
}
