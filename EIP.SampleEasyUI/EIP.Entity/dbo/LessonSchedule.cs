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
    /// 课程安排
    ///</summary>
    [Table("LessonSchedule",Schema = "dbo")]
    public partial class LessonSchedule : AggregateRoot
    {
    
        /// <summary>
        /// 课程安排ID
        /// </summary>
        [Key, Required]
        public int LSId { get; set; }
    
        /// <summary>
        /// 用来识别课程信息
        /// </summary>
        [Required]
        public int LIId { get; set; }
    
        /// <summary>
        /// 教室信息Id
        /// </summary>
        [Required]
        public int ClassroomId { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime LessonTime { get; set; }
    
        /// <summary>
        /// 班级任课教师数据ID,确定任课教师和上课学生
        ///    
        /// </summary>
        [Required]
        public int MTRId { get; set; }
    
        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 教室信息
        ///    
        /// </summary>
        [ForeignKey("ClassroomId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Classroom Classroom { get; set; }
    
        /// <summary>
        /// 课程信息表
        /// </summary>
        [ForeignKey("LIId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual LessonInfo LessonInfo { get; set; }
    
        /// <summary>
        /// 用来记录每个班级的任课教师
        /// </summary>
        [ForeignKey("MTRId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual entorToRemo entorToRemo { get; set; }
    }
}
