using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Courses_Distribution_System.Repositories;

namespace Courses_Distribution_System.Pages.Professors
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class EditModel : PageModel
    {
        private readonly IProfessorRepository _repository;

        public EditModel(IProfessorRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
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
            TempData["year"] = Professor.ProfessorHours.Last().AcademicYear;
            TempData["minHours"] = Professor.ProfessorHours.Last().MinHours;
            TempData["maxHours"] = Professor.ProfessorHours.Last().MaxHours;

            return Page();
        }

        [BindProperty]
        public ProfessorHours ProfessorHours { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool update = false;
            ProfessorHours.ProfessorId = Professor.Id;

            if (ProfessorHours.AcademicYear == (string) TempData["year"] &&
                ProfessorHours.MinHours ==(int) TempData["minHours"] &&
                ProfessorHours.MaxHours ==(int) TempData["maxHours"])
            {
                update = await _repository.UpdateProfessor(Professor);
            }
            else if (ProfessorHours.AcademicYear == (string)TempData["year"])
            {
                update = await _repository.UpdateProfessor(Professor, ProfessorHours, update);
            }
            else
            {
                update = await _repository.UpdateProfessor(Professor, ProfessorHours, !update);
            }
            if (!update)
            {
                return NotFound();
            }
            TempData.Remove("year");
            TempData.Remove("minHours");
            TempData.Remove("maxHours");
            return RedirectToPage("./Index");
        }
    }
}
