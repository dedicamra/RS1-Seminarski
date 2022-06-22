using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Autentifikacija
{
    public class AppUserVM
    {
        [Required(ErrorMessage = "Ime is required.")]
        public string ime { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        public string confirmPassword { get; set; }
    }
}
