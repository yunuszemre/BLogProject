using Microsoft.AspNetCore.Mvc;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
