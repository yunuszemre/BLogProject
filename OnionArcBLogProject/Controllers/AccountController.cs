using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Entities;
using OnionArcBLogProject.WebUI.Models.ViewModels;
using System.Security.Claims;

namespace OnionArcBLogProject.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreService<User> _userService;
        public AccountController(ICoreService<User> userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (_userService.Any(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword))
            {
                User loggedUser = _userService.GetByDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);

                //jullanıcnın saklayacağınmız bilgilerii claim'ler olarak tutablilriz

                var claims = new List<Claim>()
                {
                    new Claim("Id", loggedUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, loggedUser.FirstName),
                    new Claim(ClaimTypes.Surname, loggedUser.LastName),
                    new Claim(ClaimTypes.Email, loggedUser.UserEmail),
                    new Claim("ImageUrl", loggedUser.ImageUrl)
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = "" });

        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Title = "Kullanıcı";
            user.Status = Core.Entity.Enum.Status.None;
                if (ModelState.IsValid)
            {
                bool result = _userService.Add(user);
                user.Status = Core.Entity.Enum.Status.None;
                if (result)
                {

                    _userService.Save();
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
    }
}

