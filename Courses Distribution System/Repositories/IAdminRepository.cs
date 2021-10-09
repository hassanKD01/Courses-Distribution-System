using System.Threading.Tasks;
using Courses_Distribution_System.Models;

namespace Courses_Distribution_System.Repositories
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminAsync(string userName, string password);
        Task<bool> UpdateAdmin(Admin admin);
    }
}