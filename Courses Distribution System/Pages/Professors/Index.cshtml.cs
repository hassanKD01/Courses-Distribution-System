using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Courses_Distribution_System.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Courses_Distribution_System.ViewModels;

namespace Courses_Distribution_System.Pages.Professors
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class IndexModel : PageModel
    {
        private readonly IProfessorRepository _repository;

        public IndexModel(IProfessorRepository repository)
        {
            _repository = repository;
        }

        public IList<ProfessorsListViewModel> Professor { get;set; }

        public async Task<IActionResult> OnGetAsync(bool? archived, string sortOrder,string searchString)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["CurrentFilter"] = searchString;
            ViewData["Archived"] = archived ?? false;
            Professor = await _repository.GetProfessorsAsync<ProfessorsListViewModel>(User.Identity.Name,archived ?? false,sortOrder,searchString);
            return Page();
        }

        [BindProperty]
        public Boolean archive { get; set; }
        [BindProperty]
        public int id { get; set; }

        public async Task<IActionResult> OnPost()
        {
            await _repository.ArchiveProfessor(!archive, id);
            return await this.OnGetAsync(archive,null,null);
        }
    }
}
