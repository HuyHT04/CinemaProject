using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ServiceResponse<T> Ok(T data, string? message = null)
        {
            return new ServiceResponse<T> { Success = true, Data = data, Message = message ?? "Success" };
        }

        public static ServiceResponse<T> Fail(string message)
        {
            return new ServiceResponse<T> { Success = false, Message = message };
        }
    }
}
