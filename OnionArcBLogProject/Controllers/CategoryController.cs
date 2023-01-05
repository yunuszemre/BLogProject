using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        
        public CategoryController(ICoreService<Category> categoryService)
        {
            _categoryService = categoryService;
            
        }

        public IActionResult Index()
        {
            
            return View(_categoryService.GetActive(t0=>t0.CategoryName).ToList());
        }
    }
}
