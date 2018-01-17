/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:32   创建人：Hujunyuan
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
    /// 学生信息表
    ///</summary>
    [Table("Grade",Schema = "dbo")]
    public partial class Grade : BusinessEntity
    {
    
        /// <summary>
        /// 学生ID
        /// </summary>
        [Key, Required]
        public int SId { get; set; }
    
        /// <summary>
        /// 学生姓名
        ///    
        /// </summary>
        [Required, MaxLength(20)]
        public string StudentName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// 
        [MaxLength(2)]
        public String Sex { get; set; }

        /// <summary>
        /// 生日 
        /// </summary>

        public DateTime? Birthday { get; set; }
    
        /// <summary>
        /// 电子邮件
        /// </summary>
        [MaxLength(20)]
        public string Email { get; set; }
    
        /// <summary>
        /// 户籍所在地
        ///    
        /// </summary>
        [MaxLength(50)]
        public string Hometown { get; set; }
    
        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(20)]
        public string Phone_number { get; set; }
    
        /// <summary>
        /// 学生学号
        /// </summary>
        [Required, MaxLength(20)]
        public string StudentId { get; set; }
    
        /// <summary>
        /// 所在班级ID
        /// </summary>
        
        public int? RId { get; set; }
    
        /// <summary>
        /// 用来存储学生家庭所在地域ID
        ///    
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 班级
        /// </summary>
        [ForeignKey("RId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Remo Remo { get; set; }
    }
}
