/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2018-01-15 10:08:58   创建人：Hujunyuan
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
    public class LocalController : BaseController
    {
        /// <summary>
        /// 用来记录地域相关属性列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/local/list.cshtml");
        }
        

        /// <summary>
        /// 用来记录地域相关属性列表
        /// </summary>
        /// <returns></returns>
        [ActionName("load_add_local_form")]
        public ActionResult loadAddLocalForm(int? id)
        {
            var entity = new Local();
            entity.ParentId = id;
            return View("~/views/local/localinfo.cshtml", entity);
        }
        /// <summary>
        /// 查询用来记录地域相关属性列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_local")]
        public JsonResult QueryLocal(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var localService = this.GetService<ILocalService>();
            result.Data = localService.QueryLocal(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 用来记录地域相关属性新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new Local();
            return View("~/views/local/form.cshtml", entity);
        }

        /// <summary>
        /// 用来记录地域相关属性编辑视图
        /// </summary>
        /// <param name="localID">记录地域表中的编号</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int localID)
        {
            var entity = new Local();

            try
            {
                var localService = this.GetService<ILocalService>();
                entity = localService.Find<Local>(localID);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/local/form.cshtml", entity);
        }

        /// <summary>
        /// 用来记录地域相关属性编辑视图
        /// </summary>
        /// <param name="localID">记录地域表中的编号</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int localID)
        {
            var entity = new Local();

            try
            {
                var localService = this.GetService<ILocalService>();
                entity = localService.Find<Local>(localID);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/local/detail.cshtml", entity);
        }

        /// <summary>
        /// 用来记录地域相关属性保存数据操作
        /// </summary>
        /// <param name="model">用来记录地域相关属性视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(Local model)
        {
            var localService = this.GetService<ILocalService>();

            if (localService.SaveLocal(model)>0)
            {
                ShowMessage("I10010");
            }
            return Json(null);
        }


        /// <summary>
        /// 删除用来记录地域相关属性
        /// </summary>
        /// <param name="localID">记录地域表中的编号</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int localID)
        {
            var localService = this.GetService<ILocalService>();

            localService.LogicDelete<Local>(localID);
            ShowMessage("I10030");

            return Json(null);
        }
        [ActionName("querylocaltree")]
        public JsonResult Querylocaltree(int? id)
        {


            var localService = this.GetService<ILocalService>();
            var data = localService.Querylocal(id);
            return Json(data);
        }

        [ActionName("tree")]
        public ActionResult Tree()
        {
            return View("~/views/Local/Tree.cshtml");
        }


        /// <summary>
        /// 通过节点ID查询用来记录地域相关属性列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_local_by_id")]
        public ActionResult query_local_by_id(int id)
        {
            var entity = new Local();
            var localservice = GetService<ILocalService>();
            entity = localservice.QuerylocalById(id);
            return View("~/views/Local/localinfo.cshtml", entity);
        }
    }



}

