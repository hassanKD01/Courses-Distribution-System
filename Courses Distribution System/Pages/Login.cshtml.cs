using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Models;
using Courses_Distribution_System.Repositories;
using Courses_Distribution_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Courses_Distribution_System.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IDepartmentRepository _repository;

        public LoginModel(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<string> Departments { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Departments = await _repository.GetDepartments();
            return Page();
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.AuthenticateUser(Input);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return await this.OnGet();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,"HeadOfDepartment")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    IssuedUtc = DateTimeOffset.UtcNow
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("/Index");
            }
            return await this.OnGet();
        }

        [BindProperty]
        public Admin AdminInput { get; set; }

        public async Task<IActionResult> OnPostAdmin([FromServices]AppDbContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Admin.Where(a => a.Name.Equals(AdminInput.Name) && a.Password.Equals(AdminInput.Password)).FirstOrDefaultAsync();

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return await this.OnGet();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,"Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Admin");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false,
                    IssuedUtc = DateTimeOffset.UtcNow
                };

                await HttpContext.SignInAsync(
                    "Admin",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("/Manage/Index");
            }
            return await this.OnGet();
        }
    }
}
