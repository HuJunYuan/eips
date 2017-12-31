/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-07 01:46:15   创建人：杨杰
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
    /// 数据字典服务
    /// </summary>
    public class AppCodeMasterService : EntityBaseService, IAppCodeMasterService
    {
        #region 初始化

        /// <summary>
        /// 数据字典仓储
        /// </summary>
        private AppCodeMasterRepository appCodeMasterRepository = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uow">工作单元</param>
        /// <param name="factory">仓储创建工厂</param>
        public AppCodeMasterService(IUnitOfWork uow, IRepositoryFactory factory)
            : base(uow, factory)
        {
            appCodeMasterRepository = this.CreateRepository<AppCodeMasterRepository>();
        }

        #endregion

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <param name="model">翻页查询基本条件</param>
        /// <param name="totalCount">整体查询结果件数</param>
        /// <returns></returns>
        public List<AppCodeMaster> QueryAppCodeMaster(QueryModel model, out int totalCount)
        {
            return appCodeMasterRepository.QueryAppCodeMaster(model, out totalCount);
        }

        /// <summary>
        /// 处理数据字典
        /// 根据 id 编号判断是否是添加还是更新，然后调用相应的方法进行处理
        /// </summary>
        /// <param name="model">数据字典信息</param>
        /// <returns>处理是否成功</returns>
        [OperationLog("字典数据修改")]
        public int SaveAppCodeMaster(AppCodeMaster model)
        {
            //检查是否存在重复数据
            var entity = this.Find<AppCodeMaster>(model.Id);
            var id = model.Id;

            //更新数据 ， 否则新增数据
            if (entity != null)
            {
                //更新数据
                entity = Mapper.Map<AppCodeMaster, AppCodeMaster>(model, entity);
                this.appCodeMasterRepository.Update(entity);
            }
            else
            {
                if (this.IsRepeatDefined(model.Code, model.ParentId))
                {
                    throw new CLApplicationException("E10071",model.Code);
                }
                //录入数据
                this.appCodeMasterRepository.Insert(model);
                this.ServiceContext.Commit();//为了取得自增字段的值，手动提交事务
                id = model.Id;

            }

            return id;
        }

        /// <summary>
        /// 根据父节点 ,查询相应的导航信息
        /// </summary>
        /// <param name="model">查询模型</param>
        /// <returns>返回查询的结果</returns>
        public List<TreeDataModel> QueryAppCodeMaster(int pid, int appId)
        {
            var treeNodes = this.appCodeMasterRepository.GetCodeMasterTreeModel(pid,appId);
            var ids = treeNodes.Select(p => int.Parse(p.Id)).ToArray();
            var children = this.appCodeMasterRepository.LoadEntities(
                p => 
                    ids.Contains(p.ParentId) &&
                    p.LogicDeleteFlag == false).ToList();
            // 节点是否有下级节点
            treeNodes.ForEach(p => {
                p.State = children.Count(p1 => p1.ParentId.ToString() == p.Id) <= 0 ? "open" : "closed";
            });
            return treeNodes;
        }

        /// <summary>
        /// 根据id获取字典信息
        /// </summary>
        /// <param name="id">id信息</param>
        /// <returns>字典信息</returns>
        public TreeDataModel QueryTreeModelById(int id)
        {
            var codeMaster = this.appCodeMasterRepository.Find(id);
            var treeNode = Mapper.Map<AppCodeMaster,TreeDataModel>(codeMaster);
            // 节点是否有下级节点
            treeNode.State = this.appCodeMasterRepository.LoadEntities(
                p => p.LogicDeleteFlag == false && p.ParentId == id)
                .Count() <= 0 ? "open" : "closed";
            return treeNode;
        }

        /// <summary>
        /// 逻辑删除节点及其子节点
        /// </summary>
        /// <param name="id">字典ID</param>
        [OperationLog("字典删除")]
        public void LogicDelete(int id)
        {
            var chiren = this.appCodeMasterRepository.LoadEntities(p => p.ParentId == id).ToList();
            this.appCodeMasterRepository.LogicDelete(id);
            chiren.ForEach(p => { this.LogicDelete(p.Id); });
        }

        /// <summary>
        /// 判断是否存在重复定义code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        private bool IsRepeatDefined(string code,int parentId)
        {
            
            return this.appCodeMasterRepository.LoadEntities(
                p => p.Code == code && p.ParentId == parentId).Count() > 0;
        }
    }
}