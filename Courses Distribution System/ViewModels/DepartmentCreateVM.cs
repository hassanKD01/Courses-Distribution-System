using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_Distribution_System.ViewModels
{
    public class DepartmentCreateVM
    {
        [Required]
        [Display(Name ="Department Name")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)).+$", ErrorMessage = "Password should contain numbers, symbols, and characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
