using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAuth.Mvc.Controllers
{
    public class ProtectedController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return  View(); 
        }
    }
}