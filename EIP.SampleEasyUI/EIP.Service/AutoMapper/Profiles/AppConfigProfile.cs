using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using EIP.Model;
using EIP.Entity;
using CoreLand.Framework.Code;

namespace EIP.Service.AutoMapper.Profiles
{
    /// <summary>
    /// 应用管理对象映射
    /// </summary>
    public class AppConfigProfile : BaseProfile
    {
        protected override void Configure()
        {

            #region 应用管理

            //导航管理添加映射
            CreateMap<AppConfig, AppConfig>();

            #endregion


        }
    }
}
