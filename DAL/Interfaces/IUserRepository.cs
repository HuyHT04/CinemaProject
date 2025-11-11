using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<AspNetUser>
    {
        Task<AspNetUser?> GetByIdAsync(string id);
        Task<AspNetUser?> GetByIdStringAsync(string id);
        Task<bool> DeleteAsync(string id);
        Task<AspNetUser?> GetByEmailAsync(string email);
    }
}
