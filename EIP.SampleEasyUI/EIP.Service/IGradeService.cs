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
using EIP.Model.ViewModels;

namespace EIP.Service
{
    /// <summary>
    /// 学生信息表服务接口
    /// </summary>
    public interface IGradeService : IEntityService, IService
    {

        /// <summary>
        /// 查询学生信息表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<Grade> QueryGrade(QueryModel model, out int totalCount);


        /// <summary>
        /// 查询学生信息表,使用数据字典
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
      List<GradeViewModel> QueryGradeUseCodeManager(QueryModel model, out int totalCount);


        /// <summary>
        /// 处理学生信息表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">学生信息表信息</param>
        /// <returns>处理是否成功</returns>
        int SaveGrade(Grade model);

        /// <summary>
        /// 删除学生信息
        ///    
        /// 根据 id 编号判断是否存在这条数据，根据相应的结果进行处理
        /// </summary>
        /// <param name="model">学生信息    信息</param>
        /// <returns>处理是否成功</returns>
        bool DeleteGrade(Grade model);

        /// <summary>
        /// 查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<CountManAndWoman> QueryCountManAndWoman(QueryModel model, out int totalCount);

        /// <summary>
        /// 保存班级信息和批量插入学生
        /// </summary>
        /// <param name="model"></param>
        /// <param name="mesgbox"></param>
        /// <returns></returns>
        bool SaveClassAndStudents(StudentClassManagmentModel model, out string mesgbox);

        /// <summary>
        /// 用存储过程查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<CountManAndWoman> QueryCountManAndWomanByProc(QueryModel model, out int totalCount);


        /// <summary>
        /// 根据班级模糊查询男生女生人数，班级总人数
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        List<CountManAndWoman> QueryCountManAndWomanUseLike(QueryModel model, out int totalCount);

        void Bathremove(int[] ids);


    }
}
