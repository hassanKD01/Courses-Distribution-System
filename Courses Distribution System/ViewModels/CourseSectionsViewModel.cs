using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Courses_Distribution_System.Models;

namespace Courses_Distribution_System.ViewModels
{
    public class CourseSectionsViewModel : BaseCoursesListVM
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        [Display(Name = "Course Hours")]
        public int CourseHours { get; set; }
        [Display(Name = "TD Hours")]
        public int TdHours { get; set; }
        [Display(Name = "TP Hours")]
        public int TpHours { get; set; }
        public IEnumerable<SectionsListViewModel> Sections { get; set; }
    }
}
