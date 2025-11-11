using BLL.Common;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : IGenericService<AspNetUser>
    {
        Task<ServiceResponse<AspNetUser>> GetByIdStringAsync(string id);
        Task<ServiceResponse<bool>> DeleteStringAsync(string id);

        Task<ServiceResponse<AspNetUser>> GetByEmailAsync(string email);

        Task<ServiceResponse<AspNetUser>> RegisterAsync(string fullName, string email, string password);
        Task<ServiceResponse<AspNetUser>> AuthenticateAsync(string email, string password);
    }
}
