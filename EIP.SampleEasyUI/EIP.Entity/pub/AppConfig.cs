/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-10-10 18:13:45   创建人：杨杰
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
    /// 系统配置
    ///</summary>
    [Table("App_Config", Schema = "pub")]
    public partial class AppConfig : BusinessEntity
    {

        /// <summary>
        /// 配置项ID(Id)
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// 配置项类型(numic,bool,string,text)
        /// </summary>
        [Required, MaxLength(10)]
        public string ValueType { get; set; }

        /// <summary>
        /// 配置项Key(Key)
        /// </summary>
        [Required, MaxLength(50)]
        public string Key { get; set; }

        /// <summary>
        /// 配置项Value(Value)
        /// </summary>
        [Required, MaxLength(4000)]
        public string Value { get; set; }

        /// <summary>
        /// 描述(Describe)
        /// </summary>
        [Required, MaxLength(500)]
        public string Describe { get; set; }

        /// <summary>
        /// 应用ID(AppId)
        /// </summary>
        [Required]
        public int AppId { get; set; }

    }
}
