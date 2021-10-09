using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Courses_Distribution_System.Pages.Professors
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CreateModel : PageModel
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;

        public CreateModel(IProfessorRepository professorRepository, IDepartmentRepository departmentRepository)
        {
            _professorRepository = professorRepository;
            _departmentRepository = departmentRepository;
        }

        public List<SelectListItem> Departments { get; set; } = new();
        public async Task< IActionResult> OnGet()
        {
            var departments = await _departmentRepository.GetDepartments();
            foreach (var department in departments)
            {
                if(department!=null)
                     Departments.Add(new SelectListItem { Value = department, Text = department });
            }
            return Page();
        }

        [BindProperty]
        public Professor Professor { get; set; }
        [BindProperty]
        public ProfessorHours ProfessorHours { get; set; }
        [BindProperty]
        [Display(Name ="Departments")]
        public IEnumerable<string> SelectedDepartments { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Professor.Archived = false;
            Professor.ProfessorHours = new List<ProfessorHours> { ProfessorHours };
            Professor.ProfessorsDepartments = new List<ProfessorsDepartments>();
            foreach (var department in SelectedDepartments)
            {
                Professor.ProfessorsDepartments.Add(new ProfessorsDepartments { DepartmentName = department });
            }
            if (!await _professorRepository.CreateProfessor(Professor))
            {
                ModelState.AddModelError("Professor.Id","Another professor already exists with this Id.");
                return await this.OnGet();
            }
            return RedirectToPage("./Index");
        }
    }
}
