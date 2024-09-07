using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.Models;
using OnionArcBLogProject.WebUI.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
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
            var posts = _postService.GetActive(t0 => t0.User, t0 => t0.Comments).Select(x=> new PostModel
            {
                Id = x.Id,
                CategoryName = x.Category.CategoryName,
                ImagePath = x.ImagePath,
                CreatedDate = x.CreatedDate??default,
                PostAuthor = new UserModel
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    ImageUrl = x.User.ImageUrl
                },
                Comments = x.Comments.Select(y=>new CommentModel
                {
                    CommentText = y.CommentText,
                    CommentAuthor = new UserModel
                    {
                        FirstName = y.User.FirstName,
                        LastName = y.User.LastName,
                        ImageUrl = y.User.ImageUrl
                    }
                }).ToList(),
                PostDetail = x.PostDetail,
                Tags = x.Tags,
                Title = x.Title,
                ViewCount = x.ViewCount
            }).ToList();
            return View(posts);
        }

        public IActionResult PostsByCatId(Guid id)
        {
            return View(_postService.GetDefault(x => x.CategroyId == id));
        }
        public JsonResult Geee()
        {
            return Json("Geee");
        }
        
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
        
        public JsonResult GetPostsByCategory(Guid id)
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
        public class PostModel
        {
            public Guid Id { get; set; }
            public string Title { get; set; }

            public string PostDetail { get; set; }

            public string Tags { get; set; }

            public string? ImagePath { get; set; }

            public int ViewCount { get; set; }
            public UserModel PostAuthor { get; set; }
            public virtual string? CategoryName { get; set; }
            public DateTime CreatedDate { get; set; }

            public virtual List<CommentModel> Comments { get; set; }

        }
        public class UserModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ImageUrl { get; set; }
        }
        public class CommentModel
        {
            public string CommentText { get; set; }
            public string UserName { get; set; }
            public UserModel CommentAuthor { get; set; }
        }
    }
}