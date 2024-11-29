using API_WEB_Ejercicio3.Data;
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
using NuGet.Versioning;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System.Security.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
        private readonly IConfiguration _configuration;

        public AuthController
            (
                SignInManager<IdentityUser> signInManager,
                UserManager<IdentityUser> userManager,
                WebAPIContext webAPIContext,
                IConfiguration configuration
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _webAPIContext = webAPIContext;
            _configuration = configuration;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Auth auth)
        {
            IActionResult response = null;
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

                //user.PasswordHash = Seeder.PasswordHasher.HashPassword(user, auth.Password);
                //await _webAPIContext.Users.AddAsync(user);
                var result = await _userManager.CreateAsync(user, auth.Password);
                
                if (result.Succeeded)
                {
                        response = Ok("Usuario registrado de forma exitosa: " + auth.Email);
                    var result2 = await _userManager.AddToRoleAsync(user, "Basic");
                    if (result2.Succeeded)
                    {
                    }
                    else
                    {
                        response = BadRequest(auth);
                    }

                } else
                {
                    response = BadRequest(auth);
                }
            }            
            return response;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Auth auth)
        {
            IActionResult response = null;
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(auth.Email);
                if (user == null)
                {
                    response = Unauthorized("Credenciales no válidas");
                }
                else
                {
                    var checkPassword = await _userManager.CheckPasswordAsync(user, auth.Password);
                    if (checkPassword)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken
                            (
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddMinutes(10),
                                signingCredentials: credentials
                            );
                        response = Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                }
            }
            return response;
        }
    }
}
