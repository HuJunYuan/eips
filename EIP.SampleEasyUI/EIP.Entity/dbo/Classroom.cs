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
    /// 教室信息
        ///    
    ///</summary>
    [Table("Classroom",Schema = "dbo")]
    public partial class Classroom : BusinessEntity
    {
    
        /// <summary>
        /// 教室信息Id
        /// </summary>
        [Key, Required]
        public int ClassroomId { get; set; }
    
        /// <summary>
        /// 教室名
        /// </summary>
        [MaxLength(20)]
        public string ClassroomName { get; set; }
    
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 课程安排
        /// </summary>
        public virtual IList<LessonSchedule> LessonSchedules { get; set; }
    }
}
