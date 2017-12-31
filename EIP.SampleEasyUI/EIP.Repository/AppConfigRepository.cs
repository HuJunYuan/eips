/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-06 00:40:37   创建人：杨杰
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data;

using CoreLand.Framework;
using CoreLand.Framework.Data;
using CoreLand.Framework.Code;
using EIP.Model;
using EIP.Entity;

namespace EIP.Repository
{
    /// <summary>
    /// 系统配置仓储
    /// </summary>
    public class AppConfigRepository : DefaultRepository<AppConfig>
    {
        public AppConfigRepository(IUnitOfWork unitOfWork, IRepositoryFactory factory)
            : base(unitOfWork, factory) { }

        /// <summary>
        /// 查询系统配置
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<AppConfig> QueryAppConfig(AppConfigQueryModel model, out int totalCount)
        {
            //查询数据
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "%" : "%" + model.Key.Trim() + "%");
            string sql = "select * from pub.App_Config where LogicDeleteFlag=0 and ([Key] like @p0 or [Value] like @p0) and AppId=@p1 ";

            //分页查询必须要有排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ? "[Key]" : model.SortField;

            var appConfigs = this.LoadPageEntitiesBySql<AppConfig>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey,
                       model.AppId
                       ).ToList();

            return appConfigs;
        }

        /// <summary>
        /// 根据key和应用ID取得配置信息
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="appId">应用ID</param>
        /// <returns></returns>
        public AppConfig GetAppConfigByKey(string key, int appId)
        {
            return this.GetEntity().Where(p => p.AppId == appId && p.Key == key).FirstOrDefault();
        }

    }
}
