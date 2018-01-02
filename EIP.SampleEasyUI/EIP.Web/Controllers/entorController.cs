/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:42   创建人：Hujunyuan
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
using CoreLand.Framework.Code;

namespace EIP.Web.Controllers
{
    public class entorController : BaseController
    {
        /// <summary>
        /// 教师列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/entor/list.cshtml");
        }

        /// <summary>
        /// 查询教师列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_entor")]
        public JsonResult Queryentor(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var entorService = this.GetService<IentorService>();
            result.Data = entorService.Queryentor(model, out totalCount);
            result.Total = totalCount;


         /// var s=  CodeManger.GetCodeText(CommonConstant.CODETYPE_SEXX,"0");

            return Json(result);
        }

        /// <summary>
        /// 通过姓名查询教师列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_entor_by_name")]
        public JsonResult QueryentorByName(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var entorService = this.GetService<IentorService>();
            result.Data = entorService.QueryentorUseCodeManager(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 教师新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new entor();
            return View("~/Views/entor/form.cshtml", entity);
        }

        /// <summary>
        /// 教师编辑视图
        /// </summary>
        /// <param name="mId">教师ID</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int mId)
        {
            var entity = new entor();

            try
            {
                var entorService = this.GetService<IentorService>();
                entity = entorService.Find<entor>(mId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/entor/form.cshtml", entity);
        }

        /// <summary>
        /// 教师编辑视图
        /// </summary>
        /// <param name="mId">教师ID</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int mId)
        {
            var entity = new entor();

            try
            {
                var entorService = this.GetService<IentorService>();
                entity = entorService.Find<entor>(mId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/entor/detail.cshtml", entity);
        }

        /// <summary>
        /// 教师保存数据操作
        /// </summary>
        /// <param name="model">教师视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(entor model)
        {
            var entorService = this.GetService<IentorService>();

            if (entorService.Saveentor(model)>0)
            {
                ShowMessage("I10010");
            }
            return Json(null);
        }


        /// <summary>
        /// 删除教师
        /// </summary>
        /// <param name="mId">教师ID</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int mId)
        {
            var entorService = this.GetService<IentorService>();

            entorService.LogicDelete<entor>(mId);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
