using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAuth.Mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult LogOut()
        {
            string returnUrl = Url.Action(
                action: "LoggedOut",
                controller: "Authentication",
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

        public IActionResult LoggedOut()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}