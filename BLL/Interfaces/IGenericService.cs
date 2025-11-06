using BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<ServiceResponse<IEnumerable<T>>> GetAllAsync();
        Task<ServiceResponse<T>> GetByIdAsync(int id);
        Task<ServiceResponse<T>> CreateAsync(T entity);
        Task<ServiceResponse<T>> UpdateAsync(T entity);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
