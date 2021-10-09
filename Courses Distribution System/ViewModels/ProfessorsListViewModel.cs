using System.ComponentModel.DataAnnotations;

namespace Courses_Distribution_System.ViewModels
{
    public class ProfessorsListViewModel : BaseProfessorsVM
    {
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool Archived { get; set; }
    }
}
