/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:37   创建人：Hujunyuan
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
    /// 班级服务接口
    /// </summary>
    public interface IRemoService : IEntityService, IService
    {

        /// <summary>
        /// 查询班级
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<Remo> QueryRemo(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理班级
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">班级信息</param>
        /// <returns>处理是否成功</returns>
        int SaveRemo(Remo model);

        /// <summary>
        /// 删除班级信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool DeleteRemo(Remo model);

        /// <summary>
        /// 修改班级任课教师信息
        /// 根据 id 编号调用相应的方法进行处理
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>处理是否成功</returns>
        /// 
        bool AlterMentorToRemoByOrder(entor oldone, entor newone, Remo remo, out entorToRemo alteredMentorToRemo);

        /// <summary>
        /// 修改班级任课教师信息
        /// 根据 id 编号调用相应的方法进行处理
        /// </summary>
        /// <param name="oldone">旧教师对象</param>
        /// <param name="newone">新教师对象</param>
        /// <returns>处理是否成功</returns>
        /// 
        bool AlterMentorToRemoWithoughtOrder(entor oldone, entor newone);

        /// <summary>
        /// 处理班级任课教师表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">班级任课教师信息</param>
        /// <returns>处理是否成功</returns>
        int SaveMentorToRemo(entorToRemo model);

        /// <summary>
        /// 删除班级任课教师信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool LogicDeleteMentorToRemo(entorToRemo model);

        /// <summary>
        /// 多行删除班级信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">信息    信息</param>
        /// <returns>处理是否成功</returns>
        void MultiLineRemove(int[] ids);
    }
}
