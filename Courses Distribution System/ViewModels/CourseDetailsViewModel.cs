using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class CourseDetailsViewModel : BaseCourseVM
    {
        public int Semester { get; set; }
        [Display(Name = "Number of Sections")]
        public int SectionsCount { get; set; }
    }
}
