using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Courses_Distribution_System.Pages.Courses
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SemesterModel : PageModel
    {
        private readonly ICoursesRepository _repository;

        public SemesterModel(ICoursesRepository repository)
        {
            _repository = repository;
        }

        public IList<CoursesListViewModel> Courses { get; set; }
        [BindProperty(SupportsGet =true)]
        public int Semester { get; set; }

        public async Task<IActionResult> OnGet(bool? archived, string sortOrder, string searchString)
        {
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["Archived"] = archived ?? false;
            Courses = await _repository.GetCoursesAsync<CoursesListViewModel>(User.Identity.Name, Semester, archived ?? false, sortOrder, searchString, null);
            return Page();
        }

        [BindProperty]
        public Boolean archive { get; set; }
        [BindProperty]
        public int id { get; set; }

        public async Task<IActionResult> OnPost([FromRoute]int semester)
        {
            await _repository.ArchiveCourse(!archive, id);
            return await this.OnGet( archive, null, null);
        }
    }
}
