using Microsoft.AspNetCore.Mvc;

namespace NetCoreAuth.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "TodoItems");
        }
    }
}