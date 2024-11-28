using API_WEB_Ejercicio3.Data;
using API_WEB_Ejercicio3.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API_WEB_Ejercicio3.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace API_WEB_Ejercicio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WebAPIContext _webAPIContext;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public AuthController
            (
                SignInManager<IdentityUser> signInManager,
                UserManager<IdentityUser> userManager,
                WebAPIContext webAPIContext
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _webAPIContext = webAPIContext;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Auth auth)
        {
            IActionResult response = null;
            List<IdentityRole> roles = await _webAPIContext.Roles.ToListAsync();
            IEnumerable<string> Iroles = Iroles.AsEnumerable(roles);
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = auth.Email,
                    NormalizedUserName = auth.Email.ToUpper(),
                    Email = auth.Email,
                    NormalizedEmail = auth.Email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,

                };

                user.PasswordHash = Seeder.PasswordHasher.HashPassword(user, auth.Password);
                
                //await _webAPIContext.Users.AddAsync(user);
                var result = await _userManager.CreateAsync(user, auth.Password);
                await _userManager.AddToRolesAsync(user, 

                if (result.Succeeded)
                {
                    response = Ok("Usuario registrado de forma exitosa: " + auth.Email);

                } else
                {
                    response = BadRequest(auth);
                }
            }            
            return response;
        }
    }
}
