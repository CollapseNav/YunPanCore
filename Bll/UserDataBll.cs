using System;
using Dal.Interface;
using Model;

namespace Bll
{
    public class UserDataBll : BaseBll<UserDataInfo>
    {
        public UserDataBll(IBaseDal<UserDataInfo> user) : base(user)
        {
        }

        public bool CheckFileSize(string filesize, string userid)
        {
            var item = req.FindSingle(m => m.Id == userid);
            var uploadFileSize = long.Parse(filesize);
            var usedSize = long.Parse(item.Stored ?? "0");
            var maxSize = long.Parse(item.Cap);
            return uploadFileSize + usedSize < maxSize;
        }

        public void AddFile(string filesize, string userid)
        {
            var item = req.FindSingle(m => m.Id == userid);
            req.Update(m => m.Id == userid, u => new UserDataInfo { Stored = (long.Parse(filesize) + long.Parse(item.Stored ?? "0")).ToString() });
        }

        public UserDataInfo LoginCheck(UserDataInfo user, out bool IsExist)
        {
            IsExist = false;
            UserDataInfo item = null;
            if (user.UserName == null)
                item = req.FindSingle(m => m.UserAccount == user.UserAccount);
            else if (user.UserAccount == null)
                item = req.FindSingle(m => m.UserName == user.UserName);

            if (item == null)
                return null;
            else if (item.PassWord != user.PassWord)
            {
                IsExist = true;
                return null;
            }

            IsExist = true;
            return item;
        }

        /// <summary>
        /// 注册
        /// </summary>
        public bool RegisterUser(UserDataInfo u)
        {
            if (UserNameExist(u.UserName)) return false;
            u.FolderPath = "/" + u.UserName;
            //10G
            u.Cap = "10737418240‬";
            try
            {
                req.Add(u);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 查询账户是否存在
        /// </summary>
        public bool AccountExist(string acc)
        {
            return req.IsExist(m => m.UserAccount == acc);
        }
        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool UserNameExist(string username)
        {
            return req.IsExist(m => m.UserName == username);
        }

        public UserDataInfo GetUserDataByName(string username)
        {
            return req.FindSingle(m => m.UserName == username);
        }

        public UserDataInfo GetUserDataById(string id)
        {
            return req.FindByID(id);
        }
    }
}
