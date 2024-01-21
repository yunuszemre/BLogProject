using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnionArcBLogProject.Entities.Enum;


namespace OnionArcBLogProject.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Category> _categoryService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public PostController(ICoreService<Post> postService, ICoreService<Category> categoryService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _postService = postService;
            _categoryService = categoryService;
            _environment = environment;
        }
        // GET: PostController
        //[HttpGet("posts")]
        [HttpGet]
        public ActionResult Index()
        {
            var posts = _postService.GetActive();
            return View(posts);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.GetActive(), "Id","CategoryName");

            return View();
        }

        // POST: PostController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        //e5b5ae87-498f-4d49-6363-08dc18559a20
        public ActionResult Create(Post post, List<IFormFile> files)
        {
            ViewBag.Categories = _categoryService.GetActive();
            post.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            //alınan resimleri resi yüklemek için olş methoda gönderme
            bool imgRes;
            string imgPath = Upload.ImageUpload(files, _environment, out imgRes);

            if (imgRes)
            {
                post.ImagePath = imgPath;
            }
            else
            {
                ViewBag.MessageError = $"Resim yükleme işleminde bir hata oluştu";
                return View();
            }

            if (ModelState.IsValid)
            {
                bool result = _postService.Add(post);
                post.Status = Status.None;
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
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
            return View(post);
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
