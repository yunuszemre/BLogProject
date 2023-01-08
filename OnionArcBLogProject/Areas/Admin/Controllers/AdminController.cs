using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Controllers;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        private readonly ICoreService<Category> _catService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;
        private readonly ICoreService<Comment> _commentService;
        public AdminController(ICoreService<Category> catService, ICoreService<Post> postService, ICoreService<User> userService, ICoreService<Comment> commentService)
        {
         
            _catService = catService;
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
        }

        [Area("Admin")]
        [HttpGet("{area}/Index")]
        public IActionResult Index()
        {
            ViewBag.User = _userService.GetActive().Count;
            ViewBag.Post = _postService.GetActive().Count;
            ViewBag.Comment = _commentService.GetActive().Count;
            ViewBag.Category = _catService.GetActive().Count;

            return View();
        }
    }
}
