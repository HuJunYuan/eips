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
    public class GradeController : BaseController
    {
        /// <summary>
        /// 学生信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/grade/list.cshtml");
        }

        /// <summary>
        /// 查询学生信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_grade")]
        public JsonResult QueryGrade(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var gradeService = this.GetService<IGradeService>();
            result.Data = gradeService.QueryGradeUseCodeManager(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }


        /// <summary>
        /// 班级信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("selectRemo")]
        public ActionResult selectRemo()
        {
            return View("~/views/grade/Remolist.cshtml");
        }

        /// <summary>
        /// 班级信息表列表for学生和班级
        /// </summary>
        /// <returns></returns>
        [ActionName("selectRemoForClassAndStudent")]
        public ActionResult selectRemoForClassAndStudent()
        {
            return View("~/views/grade/RemolistForClassAndStuChoose.cshtml");
        }


        /// <summary>
        /// 班级学生信息表列表
        /// </summary>
        /// <returns></returns>
        [ActionName("ClassAndStudentChoose")]
        public ActionResult ClassAndStudentChoose()
        {
            return View("~/views/grade/ClassAndStudentChoose.cshtml");
        }

        /// <summary>
        /// 学生信息表新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new Grade();
            return View("~/views/grade/form.cshtml", entity);
        }

        /// <summary>
        /// 学生信息表编辑视图
        /// </summary>
        /// <param name="sId">学生ID</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int sId)
        {
            var entity = new Grade();

            try
            {
                var gradeService = this.GetService<IGradeService>();
                entity = gradeService.Find<Grade>(sId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message);
            }

            return View("~/views/grade/form.cshtml", entity);
        }

        /// <summary>
        /// 学生信息表编辑视图
        /// </summary>
        /// <param name="sId">学生ID</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int sId)
        {
            var entity = new Grade();

            try
            {
                var gradeService = this.GetService<IGradeService>();
                entity = gradeService.Find<Grade>(sId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message);
            }

            return View("~/views/grade/detail.cshtml", entity);
        }

        /// <summary>
        /// 学生信息表保存数据操作
        /// </summary>
        /// <param name="model">学生信息表视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(Grade model)
        {
            var gradeService = this.GetService<IGradeService>();

            if (gradeService.SaveGrade(model) > 0)
            {
                //ShowMessage("I10010");
            }
            return Json(null);
        }


        /// <summary>
        /// 删除学生信息表
        /// </summary>
        /// <param name="sId">学生ID</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int sId)
        {
            var gradeService = this.GetService<IGradeService>();

            gradeService.LogicDelete<Grade>(sId);
            ShowMessage("I10030");

            return Json(null);
        }

        [ActionName("bathremove")]
        public JsonResult Bathremove(int[] ids)
        {
            var gradeService = this.GetService<IGradeService>();
            gradeService.Bathremove(ids);

            ShowMessage("I10030");
            return Json(null);
        }

    }
}
