using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Courses_Distribution_System.Pages.Manage.Departments
{
    [Authorize(Policy ="Admin")]
    public class CreateModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public CreateModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DepartmentCreateVM Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Department department = new() { Name = Department.Name, Password = Department.Password };

            if (!await _repository.CreateDepartment(department))
            {
                ModelState.AddModelError("Department.Name", "A department with this name already exists");
                return this.OnGet();
            }
            return RedirectToPage("./Index");
        }
    }
}
