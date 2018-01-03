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
    }
}
