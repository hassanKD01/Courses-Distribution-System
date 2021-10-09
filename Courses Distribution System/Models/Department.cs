using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.Models
{
    public class Department
    {
        [Key]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public List<ProfessorsDepartments> ProfessorsDepartments { get; set; }
        public List<Course> Courses { get; set; }
    }
}
