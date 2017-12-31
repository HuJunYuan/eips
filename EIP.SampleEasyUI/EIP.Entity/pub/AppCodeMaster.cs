/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-07 16:01:46   创建人：杨杰
  描    述：此代码为自动生成代码，请不要手动修改，
            如果有特殊需要请使用partial扩展
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreLand.Framework.Authentication;
using CoreLand.Framework.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIP.Entity
{
    /// <summary>
    /// 数据字典
    ///</summary>
    [Table("App_CodeMaster",Schema = "pub")]
    public partial class AppCodeMaster : BusinessEntity
    {
    
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    
        /// <summary>
        /// 父节点ID
        /// </summary>
        [Required]
        public int ParentId { get; set; }
    
        /// <summary>
        /// 编码
        /// </summary>
        [Required, MaxLength(100)]
        public string Code { get; set; }
    
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(200)]
        public string Text { get; set; }
    
        /// <summary>
        /// 略称
        /// </summary>
        [Required, MaxLength(200)]
        public string ShortText { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string Pinyin { get; set; }
    
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required, MaxLength(14)]
        public string ShowIndex { get; set; }
    
        /// <summary>
        /// 预备字段1
        /// </summary>
        [MaxLength(200)]
        public string PrepareField1 { get; set; }
    
        /// <summary>
        /// 预备字段2
        /// </summary>
        [MaxLength(200)]
        public string PrepareField2 { get; set; }
    
        /// <summary>
        /// 预备字段3
        /// </summary>
        [MaxLength(200)]
        public string PrepareField3 { get; set; }
    
        /// <summary>
        /// 预备字段4
        /// </summary>
        [MaxLength(200)]
        public string PrepareField4 { get; set; }
    
        /// <summary>
        /// 应用ID(AppId)
        /// </summary>
        [Required]
        public int AppId { get; set; }
    

    }
}
