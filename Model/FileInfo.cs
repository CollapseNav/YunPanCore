using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("FileInfo"), Serializable]
    public class FileInfo : BaseEntity
    {

        /// <summary>
        /// 文件名/文件夹名
        /// </summary>
        [MaxLength(248), Required]
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [MaxLength(10), Required]
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [MaxLength(20), Required]
        public string FileSize { get; set; }

        /// <summary>
        /// 文件在服务端上的路径
        /// </summary>
        [MaxLength(200), Required]
        public string FilePath { get; set; }

        /// <summary>
        /// 文件Hash码
        /// </summary>
        [MaxLength(40)]
        public string HashCode { get; set; }

        /// <summary>
        /// 文件对应用户文件夹的路径
        /// </summary>
        [MaxLength(200), Required]
        public string MapPath { get; set; }

        /// <summary>
        /// 拥有者
        /// </summary>
        [MaxLength(40), Required]
        public string OwnerId { get; set; }

        /// <summary>
        /// 拥有者名
        /// </summary>
        /// <value></value>
        [MaxLength(40), Required]
        public string OwnerName { get; set; }

        // / <summary>
        // / 拥有者(第一个上传的人)
        // / </summary>
        [ForeignKey("OwnerId")]
        public virtual UserDataInfo Owner { get; set; }

        /// <summary>
        /// 是否分享
        /// </summary>
        public int Shared { get; set; }

        public virtual ICollection<SharedFileInfo> SharedFiles { get; set; }
    }
}
