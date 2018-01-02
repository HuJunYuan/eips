/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 17:22:09   创建人：Hujunyuan
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
    /// 班级
    ///</summary>
    [Table("Remo",Schema = "dbo")]
    public partial class Remo : BusinessEntity
    {
    
        /// <summary>
        /// 班级ID
        ///    
        /// </summary>
        [Key, Required]
        public int RId { get; set; }
    
        /// <summary>
        /// 班级代码编号
        /// </summary>
        [MaxLength(20)]
        public string Remo_id { get; set; }
    
        /// <summary>
        /// 班主任编号
        /// </summary>
        [MaxLength(20)]
        public string MasterNum { get; set; }
    
        /// <summary>
        /// 所属学院编号
        /// </summary>
        
        public int? CId { get; set; }
    
        /// <summary>
        /// 备注
        ///    
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 学生信息表
        /// </summary>
        public virtual IList<Grade> Grades { get; set; }
    
        /// <summary>
        /// 用来记录每个班级的任课教师
        /// </summary>
        public virtual IList<entorToRemo> entorToRemos { get; set; }
    
        /// <summary>
        /// 学院信息
        ///    
        /// </summary>
        [ForeignKey("CId")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual College College { get; set; }
    }
}
