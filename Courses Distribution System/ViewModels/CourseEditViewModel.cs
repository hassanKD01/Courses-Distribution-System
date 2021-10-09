using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class CourseEditViewModel : BaseCourseVM
    {
        public bool Archived { get; set; }
        public int Semester { get; set; }
    }
}
