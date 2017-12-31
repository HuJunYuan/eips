/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-06 00:40:37   创建人：杨杰
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CoreLand.Framework.Service;
using EIP.Model;
using EIP.Entity;

namespace EIP.Service
{
    /// <summary>
    /// 系统配置服务接口
    /// </summary>
    public interface IAppConfigService : IEntityService, IService
    {

        /// <summary>
        /// 查询系统配置
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<AppConfig> QueryAppConfig(AppConfigQueryModel model, out int totalCount);

        /// <summary>
        /// 处理系统配置
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">系统配置信息</param>
        /// <returns>处理是否成功</returns>
        int SaveAppConfig(AppConfig model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        void LogicDeleteByIds(int[] ids);
    }
}
