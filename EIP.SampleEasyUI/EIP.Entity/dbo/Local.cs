/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2018-01-12 10:32:08   创建人：Hujunyuan
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
    /// 用来记录地域相关属性
    ///</summary>
    [Table("Local",Schema = "dbo")]
    public partial class Local : CoreLand.Framework.Data.Entity
    {
    
        /// <summary>
        /// 记录地域表中的编号
        /// </summary>
        [Key, Required]
        public int LocalID { get; set; }
    
        /// <summary>
        /// 记录地域编号，类似邮编
        /// </summary>
        [MaxLength(20)]
        public string LocalNum { get; set; }
    
        /// <summary>
        /// 记录地域的中文名
        /// </summary>
        [MaxLength(20)]
        public string LocalName { get; set; }
    
        /// <summary>
        /// 当前地域记录的父级LocalID
        ///    
        /// </summary>
        [Required]
        public int? ParentId { get; set; }

        /// <summary>
        /// 当前地域记录的行政等级
        ///    
        /// </summary>
        [Required]
        public int? admin_level { get; set; }
        
    }
}
