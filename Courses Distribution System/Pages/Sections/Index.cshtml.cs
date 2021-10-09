using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses_Distribution_System.ExcelModels;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.Services;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Courses_Distribution_System.Pages.Sections
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class IndexModel : PageModel
    {
        private readonly ICoursesRepository _repository;
        private readonly IProfessorRepository _profrepository;
        private readonly ExcelService _excelService;

        public IndexModel(ICoursesRepository repository, IProfessorRepository profrepository, ExcelService excelService)
        {
            _repository = repository;
            _profrepository = profrepository;
            _excelService = excelService;
        }

        public IList<CourseSectionsViewModel> Courses { get; set; }

        [BindProperty(SupportsGet =true)]
        public string sortOrder { get; set; }
        [BindProperty(SupportsGet =true)]
        public string searchString { get; set; }
        [BindProperty(SupportsGet =true)]
        public string AcademicYear { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["AcademicYear"] = AcademicYear ?? DateTime.Now.Year.ToString() + "/" + (DateTime.Now.Year + 1).ToString();
            
            Courses = await _repository.GetCoursesAsync<CourseSectionsViewModel>(User.Identity.Name, null, false, sortOrder, searchString, ViewData["AcademicYear"].ToString());
            Professors = await _profrepository.GetProfessorsAsync<BaseProfessorsVM>(User.Identity.Name, false, "", null);

            return Page();
        }

        [BindProperty]
        public int? Id { get; set; }
        public async Task<IActionResult> OnPostDelete([FromServices]ISectionsRepository repository)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var delete = await repository.DeleteSection(Id);
            if (delete == null)
            {
                return RedirectToPage("../Error");
            }
            return await this.OnGet();
        }

        [BindProperty]
        public SectionCreateViewModel Input { get; set; }
        public IList<BaseProfessorsVM> Professors { get; set; }

        public async Task<IActionResult> OnPostAdd([FromServices] ISectionsRepository repository)
        {
            if (!ModelState.IsValid)
            {
                return await this.OnGet();
            }
            Section section = new()
            {
                Name = Input.Name,
                ProfessorId = Input.ProfessorId,
                CourseId = Input.CourseId,
                CourseHours = Input.CourseHours,
                TdHours = Input.TdHours,
                TpHours = Input.TpHours,
                AcademicYear = AcademicYear
            };

            await repository.AddSection(section);
            return await this.OnGet();
        }

        [BindProperty]
        public Section Update { get; set; }

        public async Task<IActionResult> OnPostUpdate([FromServices]ISectionsRepository repository)
        {
            bool update;

            if (Update.AcademicYear != AcademicYear)
            {
                Update.Id = 0;
                await repository.AddSection(Update);
                update = true;
            }
            else
            {
                update = await repository.UpdateSection(Update);
            }
            if (!update)
            {
                return NotFound();
            }
            return await this.OnGet();
        }

        public async Task<FileResult> OnGetExportCourses()
        {
            var data = await _repository.GetCoursesAsync<ExcelByCourse>(User.Identity.Name, null, false, "", null, AcademicYear);
            return File(_excelService.DistributionReportByCourse(data),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Distribution-by-Course.xlsx");
        }

        public async Task<FileResult> OnGetExportProfessors()
        {
            var data = await _profrepository.GetProfessorsAsync<ExcelByProf>(User.Identity.Name, false, "", null);

           return File(_excelService.DitributionReportByProfessor(data),
               "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Distribution-by-Professor.xlsx");
        }
    }
}
