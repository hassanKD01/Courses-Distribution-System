using System.Collections.Generic;
using System.Threading.Tasks;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Repositories
{
    public interface ICoursesRepository
    {
        Task<bool> AddCourse(Course course);
        Task ArchiveCourse(bool archive, int id);
        Task<int> DeleteCourse(int? id);
        Task<T> GetCourseAsync<T>(int? id, string department) where T : BaseCourseVM;
        Task<IList<T>> GetCoursesAsync<T>(string department, int? semester, bool archived, string sortOrder, string searchString, string academicYear) where T : BaseCoursesListVM;
        Task<bool> UpdateCourse(Course course);
    }
}