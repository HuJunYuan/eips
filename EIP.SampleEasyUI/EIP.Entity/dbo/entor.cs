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
    /// 教师
    ///</summary>
    [Table("Mentor", Schema = "dbo")]
    public partial class entor : BusinessEntity
    {

        /// <summary>
        /// 教师ID
        /// </summary>
        [Key, Required]
        public int MId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public int? MentorId { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [MaxLength(20)]
        public string MentorName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(2)]
        public String Sex { get; set; }

        /// <summary>
        /// 教师等级
        /// </summary>
        [Required]
        public int MentorLevel { get; set; }

        /// <summary>
        /// 备注
        ///    
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }

        /// <summary>
        /// 用来记录每个班级的任课教师
        /// </summary>
        public virtual IList<entorToRemo> entorToRemos { get; set; }
    }
}
