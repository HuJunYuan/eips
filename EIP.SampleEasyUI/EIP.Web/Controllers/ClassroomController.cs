/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-12-20 16:42:41   创建人：Hujunyuan
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
    public class ClassroomController : BaseController
    {
        /// <summary>
        /// 教室信息
        ///    列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/classroom/list.cshtml");
        }

        /// <summary>
        /// 查询教室信息
        ///    列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_classroom")]
        public JsonResult QueryClassroom(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var classroomService = this.GetService<IClassroomService>();
            result.Data = classroomService.QueryClassroom(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 教室信息
        ///    新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new Classroom();
            return View("~/views/classroom/form.cshtml", entity);
        }

        /// <summary>
        /// 教室信息
        ///    编辑视图
        /// </summary>
        /// <param name="classroomId">教室信息Id</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int classroomId)
        {
            var entity = new Classroom();

            try
            {
                var classroomService = this.GetService<IClassroomService>();
                entity = classroomService.Find<Classroom>(classroomId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/classroom/form.cshtml", entity);
        }

        /// <summary>
        /// 教室信息
        ///    编辑视图
        /// </summary>
        /// <param name="classroomId">教室信息Id</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int classroomId)
        {
            var entity = new Classroom();

            try
            {
                var classroomService = this.GetService<IClassroomService>();
                entity = classroomService.Find<Classroom>(classroomId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/classroom/detail.cshtml", entity);
        }

        /// <summary>
        /// 教室信息
        ///    保存数据操作
        /// </summary>
        /// <param name="model">教室信息
        ///    视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(Classroom model)
        {
            var classroomService = this.GetService<IClassroomService>();

            if (classroomService.SaveClassroom(model)>0)
            {
                ShowMessage("I10010", MessageFuncOption.CloseBrowserWindow);
            }
            return Json(null);
        }


        /// <summary>
        /// 删除教室信息
        ///    
        /// </summary>
        /// <param name="classroomId">教室信息Id</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int classroomId)
        {
            var classroomService = this.GetService<IClassroomService>();

            classroomService.LogicDelete<Classroom>(classroomId);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
