using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.Models;
using OnionArcBLogProject.WebUI.Models.ViewModels;
using System.Diagnostics;
using System.Web.Helpers;
using X.PagedList;

namespace OnionArcBLogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<Category> _catService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;
        private readonly ICoreService<Comment> _commentService;

        public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<Post> PostService, ICoreService<User> UserService, ICoreService<Comment> commentService)
        {
            _logger = logger;
            _catService = catService;
            _postService = PostService;
            _userService = UserService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View(_postService.GetActive(t0 => t0.User, t0 => t0.Comments).ToList());
        }

        public IActionResult PostsByCatId(Guid id)
        {
            return View(_postService.GetDefault(x => x.CategroyId == id));
        }

        [HttpGet("PostDetail/{id?}")]
        public IActionResult PostDetail(Guid id)
        {
            Post readedPost = _postService.GetById(id);
            readedPost.ViewCount += 1;
            _postService.Update(readedPost);

            PostDetailVM vm = new PostDetailVM();
            vm.Category = _catService.GetById(readedPost.CategroyId);
            vm.Post = readedPost;
            vm.User = _userService.GetById(readedPost.UserId);
            vm.Comments = _commentService.GetDefault(x => x.PostId == readedPost.Id).ToList();
            vm.Categories = _catService.GetActive().ToList();

            return View(vm);
        }
        public List<Post> GetRandomPosts()
        {
            List<Post> posts = new List<Post>();


            posts = _postService.GetActive().ToList();

            return posts;
        }
        [HttpGet]
        public IActionResult GetPostsByCategory(Guid id)
        {
            var posts = _postService.GetDefault(x => x.CategroyId == id).Select(x => new RelatedModel
            {
                Title = x.Title,
                Description = x.PostDetail,
                ViewCount = x.ViewCount
            }).ToList();
            var json = JsonConvert.SerializeObject(posts);
            return Json(posts);
        }
        class RelatedModel
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public int ViewCount { get; set; }
        }
    }
}