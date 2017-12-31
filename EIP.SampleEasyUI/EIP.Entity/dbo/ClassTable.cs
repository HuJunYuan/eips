/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-07 16:18:19   创建人：mxl
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
    /// 班级表
    ///</summary>
    [Table("ClassTable",Schema = "dbo")]
    public partial class ClassTable : CoreLand.Framework.Data.Entity
    {
    
        /// <summary>
        /// 班级ID
        /// </summary>
        [Key, Required]
        public int ClassID { get; set; }
    
        /// <summary>
        /// 班级名字
        /// </summary>
        [Required, MaxLength(30)]
        public string ClassName { get; set; }
    
        /// <summary>
        /// 学生表
        /// </summary>
        public virtual IList<Student> Students { get; set; }
    
    }
}
