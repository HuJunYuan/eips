/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-07 16:18:23   创建人：mxl
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
    /// 班级表服务
    /// </summary>
    public class ClassTableService : EntityBaseService, IClassTableService
    {
        #region 初始化

        /// <summary>
        /// 班级表仓储
        /// </summary>
        private ClassTableRepository classTableRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public ClassTableService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            classTableRepository = this.CreateRepository<ClassTableRepository>();
        }

        #endregion

        #region IClassTableService method

        /// <summary>
        /// 查询班级表
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<ClassTable> QueryClassTable(QueryModel model, out int totalCount)
        {
            return classTableRepository.QueryClassTable(model, out totalCount);
        }

        /// <summary>
        /// 处理班级表
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">班级表信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveClassTable(ClassTable model)
        {
            //检查是否存在重复数据
            var entity = this.Find<ClassTable>(model.ClassID);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                //entity = Mapper.Map<ClassTable, ClassTable>(model, entity);
                this.classTableRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.classTableRepository.Insert(model);
            }

            return model.ClassID;
        }
		#endregion

        #region private method

		#endregion
    }
}