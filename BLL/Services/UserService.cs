using BLL.Common;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Model;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Services
{
    public class UserService : GenericService<AspNetUser>, IUserService
    {

        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<AspNetUser>> GetByIdStringAsync(string id)
        {
            var response = new ServiceResponse<AspNetUser>();
            try
            {
                var user = await _repo.GetByIdStringAsync(id);
                if (user != null)
                {
                    response.Success = true;
                    response.Data = user;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteStringAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var result = await _repo.DeleteAsync(id);
                response.Success = result;
                response.Data = result;
                if (!result)
                    response.Message = "Delete failed or user not found";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<AspNetUser>> GetByEmailAsync(string email)
        {
            var response = new ServiceResponse<AspNetUser>();
            try
            {
                var user = await _repo.GetByEmailAsync(email);
                if (user != null)
                {
                    response.Success = true;
                    response.Data = user;
                }
                else
                {
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        // Register user with hashed password
        public async Task<ServiceResponse<AspNetUser>> RegisterAsync(string fullName, string email, string password)
        {
            var resp = new ServiceResponse<AspNetUser>();
            try
            {
                var existing = await _repo.GetByEmailAsync(email.Trim());
                if (existing != null)
                {
                    resp.Success = false;
                    resp.Message = "Email already registered.";
                    return resp;
                }

                var user = new AspNetUser
                {
                    Id = Guid.NewGuid().ToString("N"),
                    FullName = string.IsNullOrWhiteSpace(fullName) ? null : fullName.Trim(),
                    Email = email.Trim(),
                    PasswordHash = HashPassword(password),
                    RoleName = "Customer"
                };

                await _repo.AddAsync(user);
                resp.Success = true;
                resp.Data = user;
                resp.Message = "Registered successfully.";
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
            }
            return resp;
        }

        // Authenticate user by verifying password
        public async Task<ServiceResponse<AspNetUser>> AuthenticateAsync(string email, string password)
        {
            var resp = new ServiceResponse<AspNetUser>();
            try
            {
                var user = await _repo.GetByEmailAsync(email.Trim());
                if (user == null)
                {
                    resp.Success = false;
                    resp.Message = "Invalid credentials.";
                    return resp;
                }

                if (!VerifyPassword(password, user.PasswordHash))
                {
                    resp.Success = false;
                    resp.Message = "Invalid credentials.";
                    return resp;
                }

                resp.Success = true;
                resp.Data = user;
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.Message = ex.Message;
            }
            return resp;
        }

        // PBKDF2 hashing helpers
        // Format stored: {iterations}.{saltBase64}.{hashBase64}
        private string HashPassword(string password)
        {
            if (password == null) password = string.Empty;
            const int iterations = 100_000;
            const int saltSize = 16;
            const int hashSize = 32;

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(hashSize);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(storedHash))
                    return false;

                var parts = storedHash.Split('.', 3);
                if (parts.Length != 3) return false;

                var iterations = int.Parse(parts[0]);
                var salt = Convert.FromBase64String(parts[1]);
                var hash = Convert.FromBase64String(parts[2]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password ?? string.Empty, salt, iterations, HashAlgorithmName.SHA256);
                var computed = pbkdf2.GetBytes(hash.Length);

                var diff = 0;
                for (int i = 0; i < hash.Length; i++)
                    diff |= hash[i] ^ computed[i];
                return diff == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
