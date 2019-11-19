using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("UserDataInfo"), Serializable]
    public class UserDataInfo : BaseEntity
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        [MaxLength(50), Required]
        public string UserAccount { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MinLength(4), MaxLength(20), Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MinLength(6), MaxLength(20), Required]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [MaxLength(10)]
        public string UserType { get; set; }

        /// <summary>
        /// 用户文件夹路径
        /// </summary>
        [MaxLength(200), Required]
        public string FolderPath { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [EmailAddress, MaxLength(50)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Phone, MaxLength(40)]
        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(233)]
        public string Remark { get; set; }

        [MaxLength(64)]
        public string Cap { get; set; }

        public virtual ICollection<FileInfo> UserFiles { get; set; }
    }
}
