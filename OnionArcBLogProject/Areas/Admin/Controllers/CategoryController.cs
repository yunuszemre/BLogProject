using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        public CategoryController(ICoreService<Category> categoryService)
        {
            _categoryService= categoryService;
        }
        
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll().ToList();
            return View(categories);
        }
        [HttpGet]
        //Create Satfasono gösterecek
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        //Sayfadan gelenGelen veriyi db ye ekleyecek
        public IActionResult Create(Category category)
        {
            category.Status = Core.Entity.Enum.Status.None;
            if (ModelState.IsValid)
            {
				bool result = _categoryService.Add(category);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
                }
            }
            else
            {
				TempData["Message"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
			}    
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            
            return View();
        }
        public IActionResult Activate(Guid Id)
        {
            _categoryService.Activate(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid Id)
        {
            _categoryService.Remove(Id);
            return RedirectToAction("Index");
        }

    }
}
