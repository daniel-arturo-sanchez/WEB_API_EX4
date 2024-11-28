using API_WEB_Ejercicio3.Data;
using API_WEB_Ejercicio3.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API_WEB_Ejercicio3.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.EntityFrameworkCore;

namespace API_WEB_Ejercicio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly WebAPIContext _webAPIContext;
        
        public AuthController(SignInManager<IdentityUser> signInManager, WebAPIContext webAPIContext)
        {
            _signInManager = signInManager;
            _webAPIContext = webAPIContext;
        }

        public string OnPostAsync(string username, string password)
        {
            var users = _webAPIContext.Users.ToArrayAsync();

            //returnUrl ??= Url.Content("~/");

            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            //if (ModelState.IsValid)
            //{
            //    // This doesn't count login failures towards account lockout
            //    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
            //    if (result.Succeeded)
            //    {
            //        _logger.LogInformation("User logged in.");
            //        return LocalRedirect(returnUrl);
            //    }
            //    if (result.RequiresTwoFactor)
            //    {
            //        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            //    }
            //    if (result.IsLockedOut)
            //    {
            //        _logger.LogWarning("User account locked out.");
            //        return RedirectToPage("./Lockout");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //        return Page();
            //    }
            //}
            return "jj";
        }       
    }
}
