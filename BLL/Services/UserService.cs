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
    public class UserService : GenericService<AspNetUser>, IUserService
    {
        public UserService(IUserRepository repo) : base(repo) { }
    }
}
