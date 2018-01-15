/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2018-01-12 10:32:09   创建人：Hujunyuan
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
    /// 用来记录地域相关属性服务
    /// </summary>
    public class LocalService : EntityBaseService, ILocalService
    {
        #region 初始化

        /// <summary>
        /// 用来记录地域相关属性仓储
        /// </summary>
        private LocalRepository localRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public LocalService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            localRepository = this.CreateRepository<LocalRepository>();
        }

        #endregion

        #region ILocalService method

        /// <summary>
        /// 查询用来记录地域相关属性
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<Local> QueryLocal(QueryModel model, out int totalCount)
        {
            return localRepository.QueryLocal(model, out totalCount);
        }

        /// <summary>
        /// 处理用来记录地域相关属性
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">用来记录地域相关属性信息</param>
        /// <returns>处理是否成功</returns>
        public int SaveLocal(Local model)
        {
            //检查是否存在重复数据
            var entity = this.Find<Local>(model.LocalID);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<Local, Local>(model, entity);
                this.localRepository.Update(entity);
            }
            else
            {
                //录入数据
                this.localRepository.Insert(model);
            }

            return model.LocalID;
        }

        /// <summary>
        /// 获取地域树
        /// </summary>
        /// <param name="id"></param>
        public List<TreeDataModel> Querylocal(int? id)
        {
            if (!id.HasValue)
            {
                id= 0;
            }
          return   this.localRepository.LoadEntities(p => p.ParentId == id.Value).Select(p=>new TreeDataModel {
                Id=p.LocalID.ToString(),
                Text=p.LocalName,
                State= "closed",
            }).ToList();
        }

        

        /// <summary>
        /// 获取地域信息
        /// </summary>
        /// <param name="id"></param>
        public Local QuerylocalById(int? id)
        {
            return this.Find<Local>(id);
        }
        #endregion

        #region private method

        #endregion
    }
}