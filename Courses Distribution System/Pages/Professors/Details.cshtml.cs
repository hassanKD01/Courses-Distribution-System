using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Courses_Distribution_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Courses_Distribution_System.Repositories;

namespace Courses_Distribution_System.Pages.Professors
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetailsModel : PageModel
    {
        private readonly IProfessorRepository _repository;

        public DetailsModel(IProfessorRepository repository)
        {
            _repository = repository;
        }

        public Professor Professor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _repository.GetProfessorAsync(id, User.Identity.Name);

            if (Professor == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repository.DeleteProfessor(id);

            return RedirectToPage("./Index");
        }
    }
}
