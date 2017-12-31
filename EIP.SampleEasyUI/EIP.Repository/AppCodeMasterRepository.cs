/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-07 01:46:14   创建人：杨杰
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
    /// 数据字典仓储
    /// </summary>
    public class AppCodeMasterRepository : DefaultRepository<AppCodeMaster>
    {
        public AppCodeMasterRepository(IUnitOfWork unitOfWork, IRepositoryFactory factory)
            : base(unitOfWork, factory) { }

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<AppCodeMaster> QueryAppCodeMaster(QueryModel model, out int totalCount)
        {
            //查询数据
            var searchKey = (string.IsNullOrEmpty(model.Key) ? "%" : "%" + model.Key.Trim() + "%");
            string sql = "select * from pub.App_CodeMaster where LogicDeleteFlag=0 and TODO like @p0 ";

            //分页查询必须要有排序字段
            model.SortField = string.IsNullOrEmpty(model.SortField) ?   model.SortField : "TODO";

            var appCodeMasters = this.LoadPageEntitiesBySql<AppCodeMaster>(
                       model.PageIndex,
                       model.PageSize,
                       out totalCount,
                       sql,
                       model.SortField + " " + model.SortOrder,
                       searchKey
                       ).ToList();

            return appCodeMasters;
        }

        /// <summary>
        /// 取得数据字典
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public List<TreeDataModel> GetCodeMasterTreeModel(int pid, int appId)
        {
            return this.GetEntity().Where(p => p.ParentId == pid && p.AppId == appId)
                    .OrderBy(p => p.ShowIndex)
                    .Select(p => new TreeDataModel()
                    {
                        Id = p.Id.ToString(),
                        Text = p.Text + "(" + p.Code + ")",
                        Attributes = new { AppId = p.AppId, PId = pid.ToString() },
                        //State = p.Childrens.Count(p2 => p2.LogicDeleteFlag == false) == 0 ? "open" : "closed"
                    }).ToList();
        }
 
    }
}
