using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Bll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace YunPanCore.Controllers
{
    [AllowAnonymous]
    public class LogRegController : Controller
    {
        private readonly UserDataBll _user;

        public LogRegController(UserDataBll user)
        {
            _user = user;
        }

        // GET: LoginAndRegister
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Claims.Count() == 0)
                return View();
            return RedirectToAction("AllFile", "UserFile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Login([Bind("UserAccount,PassWord")]UserDataInfo user)
        {
            var item = _user.LoginCheck(user, out bool IsExist);
            if (!IsExist)
                return "-1";

            if (item == null)
                return "0";

            var claims = new List<Claim>(){
                    new Claim("UserId",item.Id),
                    new Claim("UserName",item.UserName),
                    new Claim("UserFolder",item.FolderPath),
                    new Claim("UserType","User")
                };

            await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies")));
            return "1";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Register([Bind("UserAccount,PassWord")] UserDataInfo user)
        {
            if (_user.AccountExist(user.UserAccount))
                return "1";

            user.UserName = user.UserAccount;
            if (_user.RegisterUser(user))
                return "0";
            return "-1";
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Index");
        }
    }
}