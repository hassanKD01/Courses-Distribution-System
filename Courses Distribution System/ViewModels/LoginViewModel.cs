using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Courses_Distribution_System.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.Text), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [DataType(DataType.Password),Display(Name ="Password")]
        public string Password { get; set; }
    }
}
