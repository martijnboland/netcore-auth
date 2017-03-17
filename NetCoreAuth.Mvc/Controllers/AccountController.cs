using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAuth.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Signin()
        {
            if (! User.Identity.IsAuthenticated)
            {
                return Challenge();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Signout()
        {
            string returnUrl = Url.Action(
                action: "Signedout",
                controller: "Account",
                values: null,
                protocol: Request.Scheme);
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult Signedout()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forbidden()
        {
            return View();
        }

    }
}