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
        public MovieService(IMovieRepository repo) : base(repo) { }
    }
}
