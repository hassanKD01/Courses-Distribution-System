using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_Distribution_System.ViewModels
{
    public class CoursesListViewModel : BaseCoursesListVM
    {
        public int Id { get; set; }
        public int Credits { get; set; }
        [Display(Name = "Course Hours")]
        public int CourseHours { get; set; }
        [Display(Name = "TD Hours")]
        public int TdHours { get; set; }
        [Display(Name = "TP Hours")]
        public int TpHours { get; set; }
        public bool Archived { get; set; }
    }
}
