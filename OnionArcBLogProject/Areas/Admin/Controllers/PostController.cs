using Microsoft.AspNetCore.Mvc;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
