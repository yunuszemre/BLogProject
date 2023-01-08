using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.WebUI.Areas.Admin.Models;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICoreService<Comment> _commentService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;

        public CommentController(ICoreService<Comment> commentService, ICoreService<Post> postService, ICoreService<User> userService)
        {
            _commentService = commentService;
            this._postService = postService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            
            return View(_commentService.GetAll(x => x.Post, x=>x.User).ToList());
        }
        public IActionResult CommentDetail(Guid Id)
        {
            CommentVM com = new CommentVM();
            com.User = _userService.GetById(_commentService.GetById(Id).UserId);
            com.Post = _postService.GetById(_commentService.GetById(Id).PostId);
            ViewBag.Comment = com;
            return View(_commentService.GetById(Id));
        }
        public IActionResult Delete(Guid Id)
        {
            _commentService.Remove(_commentService.GetById(Id));
            return RedirectToAction("Index");
        }
        public IActionResult Activate(Guid Id)
        {
           

            _commentService.Activate(Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        //Sayfadan gelenGelen veriyi db ye ekleyecek
        public IActionResult Create(Guid userId, Guid postId, string commentMessage)
        {
            Comment addedComment = new Comment();
            addedComment.UserId = userId;
            addedComment.PostId = postId;
            addedComment.CommentText= commentMessage;
            if (ModelState.IsValid)
            {
                bool result = _commentService.Add(addedComment);
                addedComment.Status = Core.Entity.Enum.Status.None;
                if (result)
                {

                    _commentService.Save();
                    TempData["MessageSuccess"] = "Kayıt işlemi başarılı";
                    RedirectToAction("Post", "Home", new { area = "", Id = postId });

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



            return RedirectToAction("Post", "Home", new {area="", Id = postId});
        }
    }
}
