using System.Collections.Generic;
using System.Threading.Tasks;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> AuthenticateUser(LoginViewModel input);
        Task<bool> CreateDepartment(Department department);
        Task DeleteDepartment(string name);
        Task<DepartmentCreateVM> GetDepartment(string name);
        Task<IEnumerable<string>> GetDepartments();
        Task<bool> UpdateDepartment(Department department);
    }
}