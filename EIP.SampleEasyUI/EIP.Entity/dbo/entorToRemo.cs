/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:33   创建人：Hujunyuan
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
    /// 用来记录每个班级的任课教师
    ///</summary>
    [Table("MentorToRemo",Schema = "dbo")]
    public partial class entorToRemo : BusinessEntity
    {
    
        /// <summary>
        /// 班级任课教师数据ID
        /// </summary>
        [Key, Required]
        public int MTRId { get; set; }
    
        /// <summary>
        /// 任课教师Id
        /// </summary>
        
        public int? MId { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        
        public int? RId { get; set; }
    
        /// <summary>
        /// 备注
        ///    
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 课程安排
        /// </summary>
        public virtual IList<LessonSchedule> LessonSchedules { get; set; }
    
        /// <summary>
        /// 教师
        /// </summary>
        [ForeignKey("MId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual entor entor { get; set; }
    
        /// <summary>
        /// 班级
        /// </summary>
        [ForeignKey("RId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual Remo Remo { get; set; }
    }
}
