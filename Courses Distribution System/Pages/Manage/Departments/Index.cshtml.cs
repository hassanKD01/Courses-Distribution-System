using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Courses_Distribution_System.Pages.Manage.Departments
{
    [Authorize(Policy ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public IndexModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> Department { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Department = await _repository.GetDepartments();
            return Page();
        }

        public async Task<IActionResult> OnPost(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            await _repository.DeleteDepartment(name);
            return await this.OnGetAsync();
        }
    }
}
