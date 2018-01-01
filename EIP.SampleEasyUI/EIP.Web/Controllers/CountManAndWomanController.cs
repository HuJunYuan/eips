/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:43   创建人：Hujunyuan
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
using EIP.Model.ViewModels;
namespace EIP.Web.Controllers
{
    public class CountManAndWomanController : BaseController
    {
        /// <summary>
        /// 学生和班级列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/CountManAndWoman/list.cshtml");
        }

        /// <summary>
        /// 查询班级学生列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_count_man_and_woman")]
        public JsonResult QueryCountManAndWoman(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var remoService = this.GetService<IGradeService>();
            result.Data = remoService.QueryCountManAndWomanUseLike(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        ///// <summary>
        ///// 班级新增视图
        ///// </summary>
        ///// <returns></returns>
        //[ActionName("add")]
        //public ActionResult Add()
        //{
        //    var entity = new Remo();
        //    return View("~/views/remo/form.cshtml", entity);
        //}
        //
        ///// <summary>
        ///// 班级编辑视图
        ///// </summary>
        ///// <param name="rId">班级ID</param>
        ///// <returns></returns>
        //[ActionName("edit")]
        //public ActionResult Edit(int rId)
        //{
        //    var entity = new Remo();
        //
        //    try
        //    {
        //        var remoService = this.GetService<IRemoService>();
        //        entity = remoService.Find<Remo>(rId);
        //
        //    }
        //    catch (CLApplicationException ex)
        //    {
        //        //修改时若发生异常则提示异常信息，并关闭修改界面
        //        this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
        //    }
        //
        //    return View("~/views/remo/form.cshtml", entity);
        //}
        //
        ///// <summary>
        ///// 班级编辑视图
        ///// </summary>
        ///// <param name="rId">班级ID</param>
        ///// <returns></returns>
        //[ActionName("detail")]
        //public ActionResult Detail(int rId)
        //{
        //    var entity = new Remo();
        //
        //    try
        //    {
        //        var remoService = this.GetService<IRemoService>();
        //        entity = remoService.Find<Remo>(rId);
        //
        //    }
        //    catch (CLApplicationException ex)
        //    {
        //        //修改时若发生异常则提示异常信息，并关闭修改界面
        //        this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
        //    }
        //
        //    return View("~/views/remo/detail.cshtml", entity);
        //}
        //
        ///// <summary>
        ///// 班级保存数据操作
        ///// </summary>
        ///// <param name="model">班级视图模型</param>
        ///// <returns></returns>
        //[ActionName("save")]
        //public JsonResult Save(Remo model)
        //{
        //    var remoService = this.GetService<IRemoService>();
        //
        //    if (remoService.SaveRemo(model)>0)
        //    {
        //        ShowMessage("I10010", MessageFuncOption.CloseBrowserWindow);
        //    }
        //    return Json(null);
        //}
        //
        //
        ///// <summary>
        ///// 删除班级
        ///// </summary>
        ///// <param name="rId">班级ID</param>
        ///// <returns>返回操作结果</returns>
        //[ActionName("remove")]
        //public JsonResult Remove(int rId)
        //{
        //    var remoService = this.GetService<IRemoService>();
        //
        //    remoService.LogicDelete<Remo>(rId);
        //    ShowMessage("I10030");
        //
        //    return Json(null);
        //}
    }
}
