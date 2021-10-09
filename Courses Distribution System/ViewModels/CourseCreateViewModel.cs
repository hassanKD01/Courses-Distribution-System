using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class CourseCreateViewModel
    {
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
        [Display(Name ="Course Hours")]
        public int CoursesHours { get; set; }
        [Required]
        [Display(Name ="TD Hours")]
        public int TdHours { get; set; }
        [Display(Name ="TP Hours")]
        [Required]
        public int TpHours { get; set; }
    }
}
