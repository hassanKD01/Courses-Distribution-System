using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_Distribution_System.Models
{
    public class Section
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [Required]
        public int CourseHours { get; set; }
        [Required]
        public int TdHours { get; set; }
        [Required]
        public int TpHours { get; set; }
        [Required]
        [MaxLength(9)]
        public string AcademicYear { get; set; }
        [Required]
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
