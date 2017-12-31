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

namespace EIP.Web.Controllers
{
    public class LessonInfoController : BaseController
    {
        /// <summary>
        /// 课程信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/lessoninfo/list.cshtml");
        }

        /// <summary>
        /// 查询课程信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_lessonInfo")]
        public JsonResult QueryLessonInfo(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var lessonInfoService = this.GetService<ILessonInfoService>();
            result.Data = lessonInfoService.QueryLessonInfo(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 课程信息表新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new LessonInfo();
            return View("~/views/lessoninfo/form.cshtml", entity);
        }

        /// <summary>
        /// 课程信息表编辑视图
        /// </summary>
        /// <param name="lIId">用来识别课程信息</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int lIId)
        {
            var entity = new LessonInfo();

            try
            {
                var lessonInfoService = this.GetService<ILessonInfoService>();
                entity = lessonInfoService.Find<LessonInfo>(lIId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/lessoninfo/form.cshtml", entity);
        }

        /// <summary>
        /// 课程信息表编辑视图
        /// </summary>
        /// <param name="lIId">用来识别课程信息</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int lIId)
        {
            var entity = new LessonInfo();

            try
            {
                var lessonInfoService = this.GetService<ILessonInfoService>();
                entity = lessonInfoService.Find<LessonInfo>(lIId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/lessoninfo/detail.cshtml", entity);
        }

        /// <summary>
        /// 课程信息表保存数据操作
        /// </summary>
        /// <param name="model">课程信息表视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(LessonInfo model)
        {
            var lessonInfoService = this.GetService<ILessonInfoService>();

            if (lessonInfoService.SaveLessonInfo(model)>0)
            {
                ShowMessage("I10010", MessageFuncOption.CloseBrowserWindow);
            }
            return Json(null);
        }


        /// <summary>
        /// 删除课程信息表
        /// </summary>
        /// <param name="lIId">用来识别课程信息</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int lIId)
        {
            var lessonInfoService = this.GetService<ILessonInfoService>();

            lessonInfoService.LogicDelete<LessonInfo>(lIId);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
