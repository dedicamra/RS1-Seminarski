using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Podaci.Klase;
using WebApplication1.Helper;
using WebApplication1.Models.Autentifikacija;
using System.Web;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AutentifikacijaController : ControllerBase
    {
        private UserManager<Korisnik> _userManager;
        private readonly ApplicationDbContext db;
        private SignInManager<Korisnik> _singInManager;
        private readonly ApplicationSettings _appSettings;
      
        public AutentifikacijaController(ApplicationDbContext Db, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            db = Db;
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Registracija([FromBody]AppUserVM model)
        {
            if (db.Kupac.Where(s => s.Email == model.email).Count() > 0)
                return BadRequest(new { message = "Email already exists." });
            var applicationUser = new Kupac()
            {
                UserName = model.email,
                Email = model.email,
                EmailConfirmed=true,
                Ime=model.ime
                //PasswordHash=model.Password.GetHashCode().ToString()
            };
            
            try
            {
                var result =await _userManager.CreateAsync(applicationUser, model.password);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM x)
        {
            var user =await  _userManager.FindByNameAsync(x.email);
            AutentifikacijaMVC.currentUserId = user.Id;
            HttpContext.SetLogiraniKorisnik(user);

            if (user != null && await _userManager.CheckPasswordAsync(user, x.password))
            {
                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID",user.Id.ToString())
                }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDecriptor);
                var token = tokenHandler.WriteToken(securityToken);
                var email = user.Email;
                return Ok(new { token,email});
            }
            else
                return BadRequest(new { message = "Username or password is not correct" });
        }
    }
}