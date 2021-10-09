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
    public class EditModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public EditModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public DepartmentCreateVM Department { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _repository.GetDepartment(id);

            if (Department == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Department department = new() { Name = Department.Name, Password = Department.Password };
            if (! await _repository.UpdateDepartment(department))
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
