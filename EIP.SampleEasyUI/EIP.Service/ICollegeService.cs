/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:36   创建人：Hujunyuan
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
    /// 学院信息
    ///    服务接口
    /// </summary>
    public interface ICollegeService : IEntityService, IService
    {

        /// <summary>
        /// 查询学院信息
        ///    
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<College> QueryCollege(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理学院信息
        ///    
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">学院信息    信息</param>
        /// <returns>处理是否成功</returns>
        int SaveCollege(College model);

        /// <summary>
        /// 删除学院信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">学院信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool DeleteCollege(College model);
    }
}
