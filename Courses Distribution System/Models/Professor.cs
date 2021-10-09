using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.Models
{
    public class Professor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        [MaxLength(20)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Middle Name is required")]
        [MaxLength(20)]
        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage ="Last Name is required")]
        [MaxLength(20)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        [Display(Name ="Backup Email")]
        public string BackupEmail { get; set; }
        [Required]
        [MaxLength(8)]
        public string PhoneNumber { get; set; }
        [MaxLength(8)]
        [Display(Name ="Backup phone number")]
        public string BackupPhoneNumber { get; set; }
        [Required(ErrorMessage ="Academic Rank required")]
        [MaxLength(12)]
        [Display(Name ="Academic Rank")]
        public string AcademicRank { get; set; }
        [Required(ErrorMessage ="Contract type required")]
        [MaxLength(10)]
        [Display(Name ="contract Type")]
        public string Contract { get; set; }

        public bool Archived { get; set; }
        public List<ProfessorHours> ProfessorHours { get; set; }
        public List<Section> Sections { get; set; }
        public List<ProfessorsDepartments> ProfessorsDepartments { get; set; }
    }
}
