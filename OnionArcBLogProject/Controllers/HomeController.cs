using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.Models;
using OnionArcBLogProject.WebUI.Models.ViewModels;
using System.Diagnostics;

namespace OnionArcBLogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<Category> _catService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;

        public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<Post> PostService, ICoreService<User> UserService)
        {
            _logger = logger;
            _catService = catService;
            _postService = PostService;
            _userService = UserService;
        }

        public IActionResult Index()
        {
            return View(_postService.GetActive(t0 => t0.User, t0 => t0.Comments).ToList());
        }

        public IActionResult PostsByCatId(Guid id)
        {
            return View(_postService.GetDefault(x => x.CategroyId == id));
        }

        public IActionResult Post(Guid id)
        {
            Post readedPost = _postService.GetById(id);
            readedPost.ViewCount += 1;
            _postService.Update(readedPost);

            PostDetailVM vm = new PostDetailVM();
            vm.Category = _catService.GetById(readedPost.CategroyId);
            vm.Post = readedPost;
            vm.User = _userService.GetById(readedPost.UserId);


            return View(vm);
        }
    }
}