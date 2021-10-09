using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminAsync(string userName, string password)
        {
            return await _context.Admin.FirstOrDefaultAsync(a => a.Name == userName && a.Password == password);
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            _context.Attach(admin).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
