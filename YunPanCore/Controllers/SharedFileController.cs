using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Bll;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace YunPanCore.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class SharedFileController : Controller
    {
        private readonly SharedFileBll _sharedFile;
        public SharedFileController(SharedFileBll sharefile)
        {
            _sharedFile = sharefile;
        }

        public IActionResult AllFile()
        {
            return View();
        }

        public string AllVideos()
        {
            return "";
        }
    }
}
