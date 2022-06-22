using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Podaci.Klase;
using WebApplication1.Data;
using WebApplication1.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
    [AllowAnonymous]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;

        public IndexModel(
            UserManager<Korisnik> userManager,
            SignInManager<Korisnik> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name ="Ime")]
            public string Ime { get; set; }

            //[Required]
            //[DataType(DataType.Text)]
            //[Display(Name = "Prezime")]
            public string Prezime { get; set; }

            [Phone]
            [RegularExpression (@"\+387\d{8,9}", ErrorMessage ="Broj treba biti u formatu +38761000111 ili +387601112233")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(Korisnik user)
        {
            KartaController.polazni = null;
            KartaController.dolazni = null;
            user = HttpContext.GetLogiranogUsera();
            var userName = await _userManager.GetUserNameAsync(user);
            userName = user.Email;
           
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Ime = user.Ime,
                //Prezime=user.Prezime,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user =await _userManager.GetUserAsync(User);//ostavila sam ovako jer async nije potpun bez await 
            user = HttpContext.GetLogiranogUsera();
            if (user == null)
            {
                return new RedirectResult("/Identity/Account/Login");
                //return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            user = HttpContext.GetLogiranogUsera();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //if (!ModelState.IsValid)
            //{
            //    await LoadAsync(user);
            //    return Page();
            //}

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if(Input.Ime != user.Ime)
            {
                user.Ime = Input.Ime;
            }
            //if (Input.Prezime != user.Prezime)
            //{
            //    user.Prezime = Input.Prezime;
            //}
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
