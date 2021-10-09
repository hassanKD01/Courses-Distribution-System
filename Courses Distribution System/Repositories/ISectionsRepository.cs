using System.Threading.Tasks;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Repositories
{
    public interface ISectionsRepository
    {
        Task AddSection(Section section);
        Task<Section> DeleteSection(int? id);
        Task<SectionsListViewModel> GetSectionAsync(int id);
        Task<bool> UpdateSection(Section section);
    }
}