using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Department> AuthenticateUser(LoginViewModel input)
        {
            return await _context.Departments.Where(dep => dep.Name == input.DepartmentName && dep.Password == input.Password).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            return departments.Select(d => new String(d.Name));
        }

        public async Task<DepartmentCreateVM> GetDepartment(string name)
        {
            return await _mapper.ProjectTo<DepartmentCreateVM>(_context.Departments).FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<bool> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException exception && (exception.Number == 2627 || exception.Number == 2601))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            _context.Attach(department).State = EntityState.Modified;
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

        public async Task DeleteDepartment(string name)
        {
            var department = await _context.Departments.FindAsync(name);

            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
