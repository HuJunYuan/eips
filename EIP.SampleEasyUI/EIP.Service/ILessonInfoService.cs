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
    /// 课程信息表服务接口
    /// </summary>
    public interface ILessonInfoService : IEntityService, IService
    {

        /// <summary>
        /// 查询课程信息表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<LessonInfo> QueryLessonInfo(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理课程信息表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">课程信息表信息</param>
        /// <returns>处理是否成功</returns>
        int SaveLessonInfo(LessonInfo model);

        /// <summary>
        /// 删除课程信息表信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">课程信息表信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool DeleteLessonInfo(LessonInfo model);
    }
}
