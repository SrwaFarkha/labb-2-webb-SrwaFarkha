using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.JSInterop;
using Webapi.Data.DataModels;
using Webapp.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Models.AccountModels;

namespace Webapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISrwasButikServices _srwasButikServices;

        public LoginController(ISrwasButikServices srwasButikServices)
        {
            _srwasButikServices = srwasButikServices;
        }

        public async Task<IActionResult> Index()
        {
            var model = new LoginModel();

            return View(model);
        }

        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var validAccount = await _srwasButikServices.CheckIfAccountExist(model);

            if (validAccount != null)
            {
                await SignIn(validAccount, true);

                return RedirectToAction("Index", "Account");
            }
            else
            {
	            ModelState.AddModelError("", "Inloggningen misslyckades, mejladressen eller lösenordet du angav är fel.");
	            return View("Index", model);

			}
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync().ConfigureAwait(false);
            
            return RedirectToAction("Index", "Home");
        }

        private Task SignIn(Account validAccount, bool remember)
        {
            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, validAccount.AccountId.ToString()),
                new Claim(ClaimTypes.Name, validAccount.FirstName),
                new Claim(ClaimTypes.Surname,validAccount.LastName),
                new Claim(ClaimTypes.Email, validAccount.EmailAddress),
                new Claim(ClaimTypes.Role, validAccount.IsAdmin.ToString()),

            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var identity = new ClaimsPrincipal(claims);
            var authProps = new AuthenticationProperties { IsPersistent = remember };

            return HttpContext.SignInAsync(identity, authProps);
        }
    }
}
