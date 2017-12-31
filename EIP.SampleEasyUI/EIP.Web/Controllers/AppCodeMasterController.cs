/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-07 01:46:16   创建人：杨杰
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using CoreLand.Framework;
using CoreLand.Framework.Service;
using CoreLand.Framework.Web;
using EIP.Entity;
using EIP.Model;
using EIP.Service;

namespace EIP.Web.Controllers
{
    public class AppCodeMasterController : BaseController
    {
        ///// <summary>
        ///// 应用列表
        ///// </summary>
        ///// <returns></returns>
        //[ActionName("apptabs")]
        //public ActionResult AppTabs()
        //{
        //    var service = this.GetService<IAppDefinitionService>();

        //    var AppInfoList = service.FindAll<AppDefinition>()
        //        .Where(p => p.CanSetCodeMaster).ToList();

        //    return View("~/views/AppCodeMaster/AppTabs.cshtml", AppInfoList);
        //}

        /// <summary>
        /// 数据字典管理列表 Action
        /// </summary>
        /// <returns></returns>
        [ActionName("codetree")]
        public ActionResult CodeTree()
        {
            return View("~/views/AppCodeMaster/CodeTree.cshtml");
        }

        /// <summary>
        /// 查询数据字典列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_appCodeMaster")]
        public JsonResult QueryAppCodeMaster(int? id, int? appId)
        {
            var dataList = new List<TreeDataModel>();

            var appCodeMasterService = this.GetService<IAppCodeMasterService>();
            id = id ?? 0;
            appId = appId ?? 0;
            dataList = appCodeMasterService.QueryAppCodeMaster(id.Value, appId.Value);


            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 数据字典编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int? id, int? appId, int? parentId)
        {
            var entity = new AppCodeMaster();

            try
            {
                if (id.HasValue)
                {
                    var appCodeMasterService = this.GetService<IAppCodeMasterService>();
                    entity = appCodeMasterService.Find<AppCodeMaster>(id.Value);
                }
                else
                {
                    entity.AppId = appId.Value;
                    entity.ParentId = parentId.Value;
                    entity.ShowIndex = DateTime.Now.ToString("yyyyMMddHHmmss");
                }


            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/appcodemaster/form.cshtml", entity);
        }

 
        /// <summary>
        /// 数据字典保存数据操作
        /// </summary>
        /// <param name="model">数据字典视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(AppCodeMaster model)
        {
            var result = new TreeDataModel();
            var appCodeMasterService = this.GetService<IAppCodeMasterService>();

            var id = appCodeMasterService.SaveAppCodeMaster(model);
            ShowMessage("I10010");
            result = appCodeMasterService.QueryTreeModelById(id);
            return Json(result);
        }


        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int id)
        {
            var appCodeMasterService = this.GetService<IAppCodeMasterService>();

            appCodeMasterService.LogicDelete(id);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
