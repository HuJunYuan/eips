/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-07 01:46:15   创建人：杨杰
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
    /// 数据字典服务接口
    /// </summary>
    public interface IAppCodeMasterService : IEntityService, IService
    {

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<AppCodeMaster> QueryAppCodeMaster(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理数据字典
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">数据字典信息</param>
        /// <returns>处理是否成功</returns>
        int SaveAppCodeMaster(AppCodeMaster model);

        /// <summary>
        /// 根据父节点 ,查询相应的导航信息
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns>返回查询的结果</returns>
        List<TreeDataModel> QueryAppCodeMaster(int pid, int appId);

        /// <summary>
        /// 根据id获取字典信息
        /// </summary>
        /// <param name="id">id信息</param>
        /// <returns>字典信息</returns>
        TreeDataModel QueryTreeModelById(int id);

        /// <summary>
        /// 逻辑删除节点及其子节点
        /// </summary>
        /// <param name="id"></param>
        void LogicDelete(int id);
    }
}
