using System;
using System.Security.Cryptography;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Bll;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace YunPanCore.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class UserFileController : Controller
    {
        private readonly UserFileBll _userfile;

        private readonly UserDataBll _userdata;

        private readonly Dictionary<string, string> FileFilter = new Dictionary<string, string>{
            {"Video",".mp4,.wmv,.rmvb"},
            {"Image",".jpg,.png"},
            {"Sound",".mp3"},
            {"Doc",".doc,.docx,.txt,.md"},
            {"Zip",".zip,.rar,.7z"}
        };

        private readonly string RootPath = @"D:/YunPanFiles";

        private static string _currentFolder = string.Empty;

        public UserFileController(UserFileBll userFile, UserDataBll userData)
        {
            _userfile = userFile;
            _userdata = userData;
        }

        [HttpGet]
        public ActionResult AllFile()
        {
            return View();
        }


        [HttpGet]
        public IActionResult DeletedFile()
        {
            return View();
        }

        [HttpGet]
        public string InitAllFiles(string folder = null)
        {
            _currentFolder = User.FindFirst(m => m.Type == "UserFolder").Value;
            if (folder != null)
                _currentFolder += "/" + folder;
            var item = _userdata.GetUserDataById(User.FindFirst(m => m.Type == "UserId").Value);
            return GetDataByFolder();
        }

        [HttpGet]
        public IActionResult DownLoadFile(string fileid)
        {
            string filepath = _userfile.GetFilePath(fileid);
            string ext = filepath.Substring(filepath.LastIndexOf('.'));
            string filename = filepath.Substring(filepath.LastIndexOf('/'));

            return File(new FileStream(filepath, FileMode.Open), new FileExtensionContentTypeProvider().Mappings[ext], filename);

        }

        [HttpGet]
        public string InitFilteFiles(string filte = null)
        {
            List<string> filter = new List<string>();
            List<Model.FileInfo> items = new List<Model.FileInfo>();

            if (filte != null)
            {
                filter = FileFilter[filte].Split(',').ToList();
                items = _userfile.GetFilesWithFilter(filter.ToArray(), User.FindFirst(match => match.Type == "UserId").Value).ToList();
            }
            else
            {
                foreach (var item in FileFilter)
                    filter.AddRange(item.Value.Split(','));
                items = _userfile.GetFilesWithNoFilter(filter.ToArray(), User.FindFirst(match => match.Type == "UserId").Value).ToList();
            }
            items = items.Count == 0 ? new List<Model.FileInfo>() : items;
            string data = Util.LayuiTableData<Model.FileInfo>(items);
            return data;
        }

        [HttpGet]
        public string DoubleClick(string folder)
        {
            _currentFolder += "/" + folder;
            return GetDataByFolder();
        }

        private string GetDataByFolder()
        {
            var items = _userfile.GetUserFilesByUserName(User.FindFirst(m => m.Type == "UserName").Value, _currentFolder).ToList();
            items = items.Count == 0 ? new List<Model.FileInfo>() : items;
            string data = Util.LayuiTableData<Model.FileInfo>(items);
            return data;
        }

        [HttpPost]
        public async Task<string> Upload()
        {
            var i = Request.Form.Files;
            IFormFile file = i.First();
            var filesize = file.Length;
            string filetype = file.FileName.Substring(file.FileName.LastIndexOf("."));
            string filename = file.FileName.Replace(filetype, "");
            string filepath = RootPath + _currentFolder;
            string ownerid = User.FindFirst(m => m.Type == "UserId").Value;
            string fullname = filepath + "/" + file.FileName;
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);
            FileStream fs = new FileStream(fullname, FileMode.CreateNew);
            try
            {
                await file.CopyToAsync(fs);
                var hash = BitConverter.ToString(MD5.Create().ComputeHash(fs)).Replace("-", "");
                var item = _userfile.IsFileExist(hash);
                if (item != null)
                    filepath = item.FilePath;

                _userfile.AddFile(new Model.FileInfo { FileName = filename, FileType = filetype, FileSize = filesize.ToString(), FilePath = filepath, MapPath = _currentFolder, OwnerName = User.FindFirst(m => m.Type == "UserName").Value, OwnerId = ownerid, IsDeleted = 0, HashCode = hash });
            }
            catch (IOException)
            {
                fs.Dispose();
                fs.Close();
                System.IO.File.Delete(fullname);
                return "1";
            }
            catch
            {
                fs.Dispose();
                fs.Close();
                System.IO.File.Delete(fullname);
                return "上传失败！";
            }
            finally
            {
                fs.Dispose();
                fs.Close();
            }
            return "1";
        }

        [HttpPost]
        public string NewFolder(string FolderName)
        {
            var filepath = RootPath + _currentFolder;
            try
            {
                Directory.CreateDirectory(filepath);
                _userfile.AddFile(new Model.FileInfo { FileName = FolderName, FileSize = "0", FileType = "Folder", FilePath = filepath, MapPath = _currentFolder, OwnerName = User.FindFirst(m => m.Type == "UserName").Value, OwnerId = User.FindFirst(m => m.Type == "UserId").Value, IsDeleted = 0 });
            }
            catch
            {
                if (Directory.Exists(filepath))
                    Directory.Delete(filepath);
                return "新建失败！";
            }
            return "新建成功！";
        }

        [HttpPost]
        public string Delete(string id)
        {
            _userfile.DeleteFile(id);
            return id;
        }

        [HttpPost]
        public string GetCurrDir()
        {
            return _currentFolder;
        }

        [HttpGet]
        public string GetSharedFiles()
        {
            string id = User.FindFirst(m => m.Type == "UserId").Value;
            var items = _userfile.GetSharedFile(id).ToList();
            items = items.Count == 0 ? new List<Model.FileInfo>() : items;
            var data = Util.LayuiTableData<Model.FileInfo>(items);
            return data;
        }

        [HttpPost]
        public string ShareFile(string id)
        {
            _userfile.ShareFile(id);
            return "OK";
        }

        [HttpPost]
        public string ShareFolder(string id)
        {
            _userfile.ShareFolder(id);
            return "OK";
        }
    }
}
