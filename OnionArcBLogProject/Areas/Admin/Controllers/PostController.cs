using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Category> _categoryService;
        public PostController(ICoreService<Post> postService, ICoreService<Category> categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var posts = _postService.GetAll().ToList();
            return View(posts);
        }
        [HttpGet]
        //Create Satfasono gösterecek
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetActive(),"Id","CategoryName");
            return View();
        }
        [HttpPost]
        //Sayfadan gelenGelen veriyi db ye ekleyecek
        public IActionResult Create(Post post)
        {

            if (ModelState.IsValid)
            {
                bool result = _postService.Add(post);
                post.Status = Core.Entity.Enum.Status.None;
                if (result)
                {

                    _postService.Save();
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



            return View(post);
        }
        [HttpPost]
        public IActionResult Update(Post post)
        {
            Post updatedPost = null;
            if (ModelState.IsValid)
            {
                updatedPost.Status = Core.Entity.Enum.Status.Updated;
                updatedPost.Title = post.Title;
                updatedPost.PostDetail = post.PostDetail;
                updatedPost.ModifiedDate = DateTime.Now;
                
                bool result = _postService.Update(post);
                
                if (result)
                {

                    _postService.Save();
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
            return View(updatedPost);
        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            return View(_postService.GetById(id));
        }
        public IActionResult Activate(Guid Id)
        {
            _postService.Activate(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid Id)
        {
            _postService.Remove(Id);
            return RedirectToAction("Index");
        }

    }
}
