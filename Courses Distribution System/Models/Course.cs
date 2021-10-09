using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_Distribution_System.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(6)]
        public string Code { get; set; }
        [Required]
        public int Credits { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public bool Optional { get; set; }
        public bool Archived { get; set; }
        [Required]
        public char Language { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public int CourseHours { get; set; }
        [Required]
        public int TdHours { get; set; }
        [Required]
        public int TpHours { get; set; }
        [ForeignKey("Department")]
        public string DepartmentName { get; set; }
        public Department Department { get; set; }
        public List<Section> Sections { get; set; }
    }
}
