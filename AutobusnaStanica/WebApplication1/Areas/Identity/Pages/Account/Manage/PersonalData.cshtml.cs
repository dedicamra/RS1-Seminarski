using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Podaci.Klase;
using WebApplication1.Helper;

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
    [AllowAnonymous]
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<Korisnik> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<Korisnik> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            user = HttpContext.GetLogiraniKorisnik();
            if (user == null)
            {
                return new RedirectResult("/Identity/Account/Login");
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}