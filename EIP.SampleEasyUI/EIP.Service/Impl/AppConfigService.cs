/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-06 00:40:38   创建人：杨杰
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
using CoreLand.Framework.Attributes;

namespace EIP.Service
{
    /// <summary>
    /// 系统配置服务
    /// </summary>
    public class AppConfigService : EntityBaseService, IAppConfigService
    {
        #region 初始化

        /// <summary>
        /// 系统配置仓储
        /// </summary>
        private AppConfigRepository appConfigRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public AppConfigService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            appConfigRepository = this.CreateRepository<AppConfigRepository>();
        }

        #endregion

        /// <summary>
        /// 查询系统配置
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<AppConfig> QueryAppConfig(AppConfigQueryModel model, out int totalCount)
        {
            return appConfigRepository.QueryAppConfig(model, out totalCount);
        }

        /// <summary>
        /// 处理系统配置
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">系统配置信息</param>
        /// <returns>处理是否成功</returns>
        [OperationLog("应用配置修改")]
        public int SaveAppConfig(AppConfig model)
        {
            //检查是否存在重复数据
            var entity = this.Find<AppConfig>(model.Id);
            var id = model.Id;
            var keyCheckEntity = this.appConfigRepository.GetAppConfigByKey(model.Key, model.AppId);

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                // 配置KEY重复定义验证
                if (keyCheckEntity != null && keyCheckEntity.Id != model.Id)
                {
                    throw new CLApplicationException("E10070", model.Key);
                }
                //更新数据
                entity = Mapper.Map<AppConfig, AppConfig>(model, entity);
                this.appConfigRepository.Update(entity);
            }
            else
            {
            
                // 配置KEY重复定义验证
                if (keyCheckEntity != null)
                {
                    throw new CLApplicationException("E10070", model.Key);
                }
                //录入数据
                this.appConfigRepository.Insert(model);
                this.ServiceContext.Commit();//为了取得自增字段的值，手动提交事务
                id = model.Id;
            }

            return id;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        [OperationLog("应用配置删除")]
        public void LogicDeleteByIds(int[] ids)
        {
            if (ids == null || ids.Length <= 0)
            {
                // 请至少选择一条记录!
                throw new CLApplicationException("W10061");
            }

            // 批量删除
            ids.ForEach(p => {
                this.LogicDelete<AppConfig>(p);
            });
        }

    }
}