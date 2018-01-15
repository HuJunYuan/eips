/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2018-01-12 10:32:09   创建人：Hujunyuan
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
    /// 用来记录地域相关属性服务接口
    /// </summary>
    public interface ILocalService : IEntityService, IService
    {

        /// <summary>
        /// 查询用来记录地域相关属性
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<Local> QueryLocal(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理用来记录地域相关属性
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">用来记录地域相关属性信息</param>
        /// <returns>处理是否成功</returns>
        int SaveLocal(Local model);


        /// <summary>
        /// 获取地域树
        /// </summary>
        /// <param name="id"></param>
        List<TreeDataModel> Querylocal(int? id);

        /// <summary>
        /// 获取地域信息
        /// </summary>
        /// <param name="id"></param>
        Local QuerylocalById(int? id);
    }
}
