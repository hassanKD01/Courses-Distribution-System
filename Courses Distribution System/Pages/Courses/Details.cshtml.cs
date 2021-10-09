using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Courses_Distribution_System.Pages.Courses
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DetailsModel : PageModel
    {
        private readonly ICoursesRepository _repository;

        public DetailsModel(ICoursesRepository repository)
        {
            _repository = repository;
        }

        public CourseDetailsViewModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _repository.GetCourseAsync<CourseDetailsViewModel>(id, User.Identity.Name);

            if (Course == null)
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
            var semester = await _repository.DeleteCourse(id);
            if (semester < 0)
            {
                return RedirectToPage("../Error");
            }

            return Redirect($"../Courses/Semester/{semester}");
        }
    }
}
