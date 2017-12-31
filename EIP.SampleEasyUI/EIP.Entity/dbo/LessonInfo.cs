/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 17:22:08   创建人：Hujunyuan
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
    /// 课程信息表
    ///</summary>
    [Table("LessonInfo",Schema = "dbo")]
    public partial class LessonInfo : BusinessEntity
    {
    
        /// <summary>
        /// 用来识别课程信息
        /// </summary>
        [Key, Required]
        public int LIId { get; set; }
    
        /// <summary>
        /// 课程编号
        /// </summary>
        [Required]
        public int LessonId { get; set; }
    
        /// <summary>
        /// 课程名字
        /// </summary>
        [MaxLength(20)]
        public string LessonName { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        [Required, MaxLength(20)]
        public string category { get; set; }
    
        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 课程安排
        /// </summary>
        public virtual IList<LessonSchedule> LessonSchedules { get; set; }
    }
}
