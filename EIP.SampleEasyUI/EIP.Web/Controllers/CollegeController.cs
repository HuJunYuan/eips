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
    public class CollegeController : BaseController
    {
        /// <summary>
        /// 学院信息
        ///    列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/college/list.cshtml");
        }

        /// <summary>
        /// 查询学院信息
        ///    列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_college")]
        public JsonResult QueryCollege(QueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var collegeService = this.GetService<ICollegeService>();
            result.Data = collegeService.QueryCollege(model, out totalCount);
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 学院信息
        ///    新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add()
        {
            var entity = new College();
            return View("~/Views/College/form.cshtml", entity);
        }

        /// <summary>
        /// 学院信息
        ///    编辑视图
        /// </summary>
        /// <param name="cId">学院信息表编号</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int cId)
        {
            var entity = new College();

            try
            {
                var collegeService = this.GetService<ICollegeService>();
                entity = collegeService.Find<College>(cId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/college/form.cshtml", entity);
        }

        /// <summary>
        /// 学院信息
        ///    编辑视图
        /// </summary>
        /// <param name="cId">学院信息表编号</param>
        /// <returns></returns>
        [ActionName("detail")]
        public ActionResult Detail(int cId)
        {
            var entity = new College();

            try
            {
                var collegeService = this.GetService<ICollegeService>();
                entity = collegeService.Find<College>(cId);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/college/detail.cshtml", entity);
        }

        /// <summary>
        /// 学院信息
        ///    保存数据操作
        /// </summary>
        /// <param name="model">学院信息
        ///    视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(College model)
        {
            var collegeService = this.GetService<ICollegeService>();

            if (collegeService.SaveCollege(model)>0)
            {
                ShowMessage("I10010", MessageFuncOption.CloseBrowserWindow);
            }
            return Json(null);
        }


        /// <summary>
        /// 删除学院信息
        ///    
        /// </summary>
        /// <param name="cId">学院信息表编号</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int cId)
        {
            var collegeService = this.GetService<ICollegeService>();

            collegeService.LogicDelete<College>(cId);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
