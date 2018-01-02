/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 17:22:13   创建人：Hujunyuan
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
using EIP.Model.ViewModels;
namespace EIP.Service
{
    /// <summary>
    /// 教师服务接口
    /// </summary>
    public interface IentorService : IEntityService, IService
    {

        /// <summary>
        /// 查询教师
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<entor> Queryentor(QueryModel model, out int totalCount);




        /// <summary>
        /// 查询教师并且使用数据字典修改性别
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<entorViewModel> QueryentorUseCodeManager(QueryModel model, out int totalCount);


        /// <summary>
        /// 查询姓名查询教师
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<entor> QueryentorByName(QueryModel model, out int totalCount);

        /// <summary>
        /// 处理教师
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>处理是否成功</returns>
        int Saveentor(entor model);

        /// <summary>
        /// 删除教师信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">教师信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool Deleteentor(entor model);
    }
}
