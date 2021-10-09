using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_Distribution_System.Models
{
    public class ProfessorHours
    {
        [MaxLength(9)]
        [Display(Name ="Academic Year")]
        public string AcademicYear { get; set; }
        [Required]
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        [Required]
        [Display(Name ="Maximum Hours")]
        public int MaxHours { get; set; }
        [Required]
        [Display(Name ="Minimum Hours")]
        public int MinHours { get; set; }
    }
}
