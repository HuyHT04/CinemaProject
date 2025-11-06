using BLL.Common;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepository<T> _repository;

        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<IEnumerable<T>>> GetAllAsync()
        {
            var result = await _repository.GetAllAsync();
            return ServiceResponse<IEnumerable<T>>.Ok(result);
        }

        public async Task<ServiceResponse<T>> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return ServiceResponse<T>.Fail("Not found");
            return ServiceResponse<T>.Ok(item);
        }

        public async Task<ServiceResponse<T>> CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return ServiceResponse<T>.Ok(entity, "Created successfully");
        }

        public async Task<ServiceResponse<T>> UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            return ServiceResponse<T>.Ok(entity, "Updated successfully");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return ServiceResponse<bool>.Ok(true, "Deleted successfully");
        }
    }
}
