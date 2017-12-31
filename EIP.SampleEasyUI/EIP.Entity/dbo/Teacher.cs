/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-07 16:18:20   创建人：mxl
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
    /// 老师表
    ///</summary>
    [Table("Teacher",Schema = "dbo")]
    public partial class eacher : BusinessEntity
    {
    
        /// <summary>
        /// 老师ID
        /// </summary>
        [Key, Required]
        public int TeacherID { get; set; }
    
        /// <summary>
        /// 老师名字
        /// </summary>
        [Required, MaxLength(30)]
        public string TeacherName { get; set; }
    
        /// <summary>
        /// 老师性别(sex)
        /// </summary>
        [Required, MaxLength(10)]
        public string Sex { get; set; }
    
        /// <summary>
        /// 老师年龄(age)
        /// </summary>
        [Required]
        public int Age { get; set; }
    
        /// <summary>
        /// 教学科目(subject)
        /// </summary>
        [Required, MaxLength(30)]
        public string Subject { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        [Required, MaxLength(50)]
        public string HomeAddress { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        [Required, MaxLength(30)]
        public string Phone { get; set; }
    
    }
}
