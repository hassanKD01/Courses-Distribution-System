using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Courses_Distribution_System.ViewModels;
using Courses_Distribution_System.Repositories;

namespace Courses_Distribution_System.Pages.Courses
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CreateModel : PageModel
    {
        private readonly ICoursesRepository _repository;

        public CreateModel(ICoursesRepository repository)
        {
            _repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public int Semester { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CourseCreateViewModel Course { get; set; }

        public async Task<IActionResult> OnPostAsync([FromRoute] int semester)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Course course = new()
            {
                Code = Course.Code,
                DepartmentName = User.Identity.Name,
                Description = Course.Description,
                Semester = semester,
                Optional = Course.Optional,
                Archived = false,
                Language = Course.Language,
                Credits = Course.Credits,
                CourseHours = Course.CoursesHours,
                TdHours = Course.TdHours,
                TpHours = Course.TpHours
            };

            if (!await _repository.AddCourse(course))
            {
                ModelState.AddModelError("Course.Code", "Another course exists with the same code and language");
                return Page();
            }
            return Redirect($"../Semester/{semester}");
        }
    }
}
