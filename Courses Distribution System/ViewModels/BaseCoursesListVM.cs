using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class BaseCoursesListVM
    {
        public string Code { get; set; }
        public char Language { get; set; }
    }
}