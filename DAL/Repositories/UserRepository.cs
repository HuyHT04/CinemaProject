using DAL.Data;
using DAL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {
        public UserRepository(CinemaBookingRazorContext context) : base(context) { }
    }
}
