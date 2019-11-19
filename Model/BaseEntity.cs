using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Serializable]
    public class BaseEntity
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, Display(Name = "主键ID"), MaxLength(40), Required]
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataType(DataType.DateTime), Required]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DeleteDate { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        [MaxLength(40)]
        public string ChangedBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataType(DataType.DateTime), Required]
        public DateTime ChangedDate { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        [Required]
        public int IsDeleted { get; set; }
    }
}
