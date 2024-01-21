using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        private readonly ICoreService<Post> _postService;

        public CategoryController(ICoreService<Category> categoryService, ICoreService<Post> postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        public IActionResult Index()
        {

            return View(_categoryService.GetActive(t0 => t0.Posts).ToList());
        }
        public IActionResult ShowPosts(Guid id)
        {

            return View(_postService.GetActive(t0 => t0.User, t2 => t2.Comments).Where(t0 => t0.CategroyId == id).ToList());
        }
        public IActionResult SearchResult(string q)
        {
              return View(_postService.GetDefault(x=>x.Title.Contains(q)));
        }
        public IActionResult GetCats(Guid id)
        {
            var catIds = _categoryService.GetDefault(x=>x.Id == id).Select(x => new
            {
                categoryId = x.Id,
                categoryName = x.CategoryName
            }).ToList();
            return Json(catIds);
        }
    }
}
