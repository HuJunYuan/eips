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
    /// 学生表
    ///</summary>
    [Table("Student",Schema = "dbo")]
    public partial class Student : BusinessEntity
    {
    
        /// <summary>
        /// 学生ID
        /// </summary>
        [Key, Required]
        public int StudentID { get; set; }
    
        /// <summary>
        /// 班级ID
        /// </summary>
        [Required]
        public int ClassID { get; set; }
    
        /// <summary>
        /// 学生名字
        /// </summary>
        [Required, MaxLength(30)]
        public string StudentName { get; set; }
    
        /// <summary>
        /// 学生性别
        /// </summary>
        [Required, MaxLength(10)]
        public string Sex { get; set; }
    
        /// <summary>
        /// 学生年龄
        /// </summary>
        [Required]
        public int Age { get; set; }
    
        /// <summary>
        /// 父亲名字(fatherName)
        /// </summary>
        [Required, MaxLength(30)]
        public string FatherName { get; set; }
    
        /// <summary>
        /// 母亲(matherName)
        /// </summary>
        [Required, MaxLength(30)]
        public string MatherName { get; set; }
    
        /// <summary>
        /// 家庭地址(HomeAddress)
        /// </summary>
        [Required, MaxLength(50)]
        public string HomeAddress { get; set; }
    
    
        /// <summary>
        /// 班级表
        /// </summary>
        [ForeignKey("ClassID")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ClassTable ClassTable { get; set; }
    }
}
