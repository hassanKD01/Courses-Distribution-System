using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Courses_Distribution_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Courses_Distribution_System.Pages.Manage
{
    [Authorize(Policy ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly IAdminRepository _repository;

        public IndexModel(IAdminRepository repository)
        {
            _repository = repository;
        }

        public string UserName { get; set; }

        [BindProperty]
        public InputModel Account { get; set; }

        public IActionResult OnGet()
        {
            UserName = User.Identity.Name;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var admin = await _repository.GetAdminAsync(Account.Name, Account.OldPassword);
            if(admin == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect User name or password.");
                return this.OnGet();
            }
            admin.Password = Account.Password;
            if (!await _repository.UpdateAdmin(admin))
            {
                ModelState.AddModelError(string.Empty, "Update Failed.");
                return this.OnGet();
            }
            return this.OnGet();
        }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            [Required]
            [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)).+$", ErrorMessage = "Password should contain numbers, symbols, and characters")]
            [DataType(DataType.Password),Display(Name ="Old Password")]
            public string OldPassword { get; set; }
            [Required]
            [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)).+$", ErrorMessage = "Password should contain numbers, symbols, and characters")]
            [DataType(DataType.Password),Display(Name ="New Password")]
            public string Password { get; set; }
            [Required]
            [Compare(nameof(Password), ErrorMessage = "Make sure that the password and the confirmation are the same.")]
            [DataType(DataType.Password),Display(Name ="Confirm new Password")]
            public string PasswordConfirmation { get; set; }
        }
    }
}
