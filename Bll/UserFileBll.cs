using System.Collections.Generic;
using System.Linq;

using Dal.Interface;

using Model;

namespace Bll
{
    public class UserFileBll : BaseBll<FileInfo>
    {
        public UserFileBll(IBaseDal<FileInfo> file) : base(file)
        { }

        public IQueryable<FileInfo> GetFilesWithFilter(string[] filters, string userid)
        {
            var data = from fi in req.GetContext().FileInfos
                       where filters.Contains(fi.FileType) && fi.OwnerId == userid
                       select fi;
            return data.Where(m => m.IsDeleted == 0);
        }

        public string GetFilePath(string fileid)
        {
            var item = req.FindByID(fileid);
            return item.FilePath + "/" + item.FileName + item.FileType;
        }

        public IQueryable<FileInfo> GetFilesWithNoFilter(string[] filters, string userid)
        {
            var data = from fi in req.GetContext().FileInfos
                       where !filters.Contains(fi.FileType) && fi.OwnerId == userid
                       select fi;
            return data.Where(m => m.IsDeleted == 0 && m.FileType != "Folder");
        }

        public IQueryable<FileInfo> GetSharedFile(string id)
        {
            return req.FindAll(m => m.OwnerId == id && m.IsDeleted == 0 && m.FileType != "Folder" && m.Shared == 1);
        }

        /// <summary>
        /// 获取Owner的所有文件信息
        /// </summary>
        public IQueryable<FileInfo> GetUserFilesByUserID(string ownerid, string folder)
        {
            return req.FindAll(m => m.OwnerId == ownerid && m.MapPath == folder).Where(m => m.IsDeleted == 0);
        }

        public IQueryable<FileInfo> GetUserFilesByUserName(string ownername, string folder)
        {
            return req.FindAll(m => m.Owner.UserName == ownername && m.MapPath == folder).Where(m => m.IsDeleted == 0);
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        public void AddFile(FileInfo file)
        {
            file.ChangedBy = file.OwnerId;
            req.Add(file);
        }

        public FileInfo IsFileExist(string hash)
        {
            return req.FindSingle(m => m.HashCode == hash && m.FileType != "Folder");
        }

        public void ShareFile(string id)
        {
            req.Update(m => m.Id == id, u => new FileInfo { Shared = 1 });
        }

        public void ShareFolder(string id)
        {
            var item = req.FindByID(id);
            string mappath = item.MapPath + "/" + item.FileName;

            req.Update(m => m.MapPath.Contains(mappath), u => new FileInfo { Shared = 1 });

            // req.Update(m => m.Id == id, u => new FileInfo { Shared = 1 });
        }
        public void UShareFile(string id)
        {
            req.Update(m => m.Id == id, u => new FileInfo { Shared = 0 });
        }

        public void DeleteFile(string id)
        {
            req.Update(m => m.Id == id, u => new FileInfo { IsDeleted = 1 });
        }

        public void UDeleteFile(string id)
        {
            req.Update(m => m.Id == id, u => new FileInfo { IsDeleted = 0 });
        }

        public IQueryable<FileInfo> GetDeletedFiles(string userid)
        {
            return req.FindAll(m => m.OwnerId == userid && m.IsDeleted == 1);
        }

        public void TrueDeleteFile(string fileid)
        {
            req.Delete(m => m.Id == fileid);
        }

        public void TrueDeleteFiles(string userid)
        {
            req.Delete(m => m.OwnerId == userid && m.IsDeleted == 1);
        }

        public void TrueDeleteFiles(List<string> files)
        {
            foreach (var item in files)
                TrueDeleteFile(item);
        }
    }
}
