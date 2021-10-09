using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class SectionCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        public int CourseId { get; set; }
        [Display(Name ="Course Hours")]
        [Required]
        public int CourseHours { get; set; }
        [Display(Name = "Td Hours")]
        [Required]
        public int TdHours { get; set; }
        [Display(Name = "Tp Hours")]
        [Required]
        public int TpHours { get; set; }
        public int ProfessorId { get; set; }
    }
}
