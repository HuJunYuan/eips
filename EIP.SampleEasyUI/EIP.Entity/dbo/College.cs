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
    /// 学院信息
        ///    
    ///</summary>
    [Table("College",Schema = "dbo")]
    public partial class College : BusinessEntity
    {
    
        /// <summary>
        /// 学院信息表编号
        /// </summary>
        [Key, Required]
        public int CId { get; set; }
    
        /// <summary>
        /// 学院编号
        /// </summary>
        [Required]
        public int CollegeId { get; set; }
    
        /// <summary>
        /// 学院名
        /// </summary>
        [Required, MaxLength(20)]
        public string CollegeName { get; set; }
    
        /// <summary>
        /// 学院地址
        /// </summary>
        [MaxLength(50)]
        public string CollegeAddress { get; set; }
    
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(16)]
        public string Other { get; set; }
    
        /// <summary>
        /// 班级
        /// </summary>
        public virtual IList<Remo> Remos { get; set; }
    }
}
