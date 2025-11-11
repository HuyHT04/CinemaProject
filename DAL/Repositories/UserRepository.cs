using DAL.Data;
using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {
        private readonly CinemaBookingRazorContext _context;

        public UserRepository(CinemaBookingRazorContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AspNetUser?> GetByIdAsync(string id)
        {
            return await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Lấy user theo string Id
        public async Task<AspNetUser?> GetByIdStringAsync(string id)
        {
            return await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        // Xóa user theo string Id
        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return false;

            _context.AspNetUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<AspNetUser?> GetByEmailAsync(string email)
        {
            return await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
