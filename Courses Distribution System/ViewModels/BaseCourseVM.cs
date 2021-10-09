using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public abstract class BaseCourseVM
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int Credits { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Optional { get; set; }
        [Required]
        public char Language { get; set; }
        [Required]
        [Display(Name = "Course Hours")]
        public int CourseHours { get; set; }
        [Required]
        [Display(Name = "TD Hours")]
        public int TdHours { get; set; }
        [Required]
        [Display(Name = "TP Hours")]
        public int TpHours { get; set; }
        public string DepartmentName { get; set; }
    }
}