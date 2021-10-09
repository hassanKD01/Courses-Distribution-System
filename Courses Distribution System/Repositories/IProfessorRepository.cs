using System.Collections.Generic;
using System.Threading.Tasks;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Repositories
{
    public interface IProfessorRepository
    {
        Task ArchiveProfessor(bool archive, int id);
        Task<bool> CreateProfessor(Professor professor);
        Task DeleteProfessor(int? id);
        Task<Professor> GetProfessorAsync(int? id, string department);
        Task<IList<T>> GetProfessorsAsync<T>(string department, bool archived, string sortOrder, string searchString) where T : BaseProfessorsVM;
        Task<bool> UpdateProfessor(Professor professor);
        Task<bool> UpdateProfessor(Professor professor, ProfessorHours professorHours, bool add);
    }
}