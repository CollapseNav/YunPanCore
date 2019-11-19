using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model {
    [Table("SharedFileInfo")]
    public class SharedFileInfo : BaseEntity {

        /// <summary>
        /// 贡献者名称(可匿)
        /// </summary>
        [MaxLength(20)]
        public string OwnerName { get; set; }


        /// <summary>
        /// 文件Id
        /// </summary>
        [MaxLength(40)]
        public string FileId { get; set; }

        /// <summary>
        /// 用户文件
        /// </summary>
        [ForeignKey("FileId")]
        public virtual FileInfo UserFile { get; set; }

    }
}