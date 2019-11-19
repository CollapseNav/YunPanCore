using System.Collections.Generic;
using System.Threading.Tasks;
using Bll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YunPanCore.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class UserDataController : Controller
    {
        private readonly UserDataBll _user;
        public UserDataController(UserDataBll user)
        {
            _user = user;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetUserName()
        {
            return User.FindFirst(m => m.Type == "UserName").Value;
        }

        [HttpPost]
        public string GetUserId()
        {
            return User.FindFirst(m => m.Type == "UserId").Value;
        }

        [HttpPost]
        public string GetUserData()
        {
            var data = _user.GetUserDataById(User.FindFirst(m => m.Type == "UserId").Value);
            return Util.SerializeData(data);
        }
    }
}