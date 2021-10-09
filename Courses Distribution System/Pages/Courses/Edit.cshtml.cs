using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper;

namespace Courses_Distribution_System.Pages.Courses
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class EditModel : PageModel
    {
        private readonly ICoursesRepository _repository;
        private readonly IMapper _mapper;

        public EditModel(ICoursesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [BindProperty]
        public CourseEditViewModel Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _repository.GetCourseAsync<CourseEditViewModel>(id,User.Identity.Name);

            if (Course == null)
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

            Course course = _mapper.Map<Course>(Course);
            course.DepartmentName = User.Identity.Name;
           
            if (! await _repository.UpdateCourse(course))
            {
                return NotFound();
            }

            return Redirect($"../Courses/Semester/{Course.Semester}");
        }
    }
}
