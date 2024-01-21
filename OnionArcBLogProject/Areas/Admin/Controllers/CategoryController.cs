using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        public CategoryController(ICoreService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll().ToList();
            return View(categories);
        }
        [HttpGet]
        //Create Satfasono gösterecek
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //Sayfadan gelenGelen veriyi db ye ekleyecek
        public IActionResult Create(Category category)
        {

            if (ModelState.IsValid)
            {
                bool result = _categoryService.Add(category);
                category.Status = Entities.Enum.Status.None;
                if (result)
                {

                    _categoryService.Save();
                    TempData["MessageSuccess"] = "Kayıt işlemi başarılı";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["MessageError"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
                }
            }
            else
            {
                TempData["MessageError"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
            }



            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            
            if (ModelState.IsValid)
            {
                bool result = _categoryService.Update(category);
                category.Status = Entities.Enum.Status.Updated;
                if (result)
                {

                    _categoryService.Save();
                    TempData["MessageSuccess"] = "Kayıt işlemi başarılı";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["MessageError"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
                }
            }
            else
            {
                TempData["MessageError"] = "Kayıt işleminde bir hata meydana geldi lütfen bilgileri kontrol ediniz";
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            return View(_categoryService.GetById(id));
        }
        public IActionResult Activate(Guid Id)
        {
            _categoryService.Activate(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid Id)
        {
            _categoryService.Remove(_categoryService.GetById(Id));
            return RedirectToAction("Index");
        }

    }
}
