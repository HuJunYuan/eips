/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-07 16:18:24   创建人：mxl
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;
using AutoMapper;

using CoreLand.Framework;
using CoreLand.Framework.Data;
using CoreLand.Framework.Service;
using CoreLand.Framework.Code;
using CoreLand.Framework.Authentication;
using CoreLand.Framework.Helpers;
using EIP.Model;
using EIP.Repository;
using EIP.Entity;

namespace EIP.Service
{
    /// <summary>
    /// 学生表服务
    /// </summary>
    public class StudentService : EntityBaseService, IStudentService
    {
        #region 初始化

        /// <summary>
        /// 学生表仓储
        /// </summary>
        private StudentRepository studentRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public StudentService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            studentRepository = this.CreateRepository<StudentRepository>();
        }

        #endregion

        #region IStudentService method

        /// <summary>
        /// 查询学生表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<Student> QueryStudent(QueryModel model, out int totalCount)
        {
            return studentRepository.QueryStudent(model, out totalCount);
        }

        /// <summary>
        /// 处理学生表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">学生表信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveStudent(Student model)
        {
            //检查是否存在重复数据
            var entity = this.Find<Student>(model.StudentID);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                //entity = Mapper.Map<Student, Student>(model, entity);
                this.studentRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.studentRepository.Insert(model);
            }

            return model.StudentID;
        }
		#endregion

        #region private method

		#endregion
    }
}