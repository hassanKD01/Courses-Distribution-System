using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses_Distribution_System.Models
{
    public class ProfessorsDepartments
    {
        [Required]
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }
        [Required]
        [ForeignKey("Department")]
        public string DepartmentName { get; set; }
        public Department Department { get; set; }
    }
}
