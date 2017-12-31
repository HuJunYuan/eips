/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-06 00:40:38   创建人：杨杰
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
    public class AppConfigController : BaseController
    {
        //[ActionName("apptabs")]
        //public ActionResult AppTabs()
        //{
        //    var service = this.GetService<IAppDefinitionService>();

        //    var AppInfoList = service.FindAll<AppDefinition>()
        //        .Where(p => p.CanSetAppConfig).ToList();

        //    return View("~/views/AppConfig/AppTabs.cshtml", AppInfoList);
        //}

        /// <summary>
        /// 系统配置列表
        /// </summary>
        /// <returns></returns>
        [ActionName("list")]
        public ActionResult List()
        {
            return View("~/views/appconfig/list.cshtml");
        }

        /// <summary>
        /// 查询系统配置列表
        /// </summary>
        /// <returns></returns>
        [ActionName("query_appConfig")]
        public JsonResult QueryAppConfig(AppConfigQueryModel model)
        {
            var result = new QueryResultModel();
            int totalCount = 0;

            var appConfigService = this.GetService<IAppConfigService>();
            var list = appConfigService.QueryAppConfig(model, out totalCount);
            list.ForEach(p => {
                if (p.ValueType == "richtext" || p.ValueType == "text")
                {
                    p.Value = Server.HtmlEncode(p.Value);
                }
            });
            result.Data = list;
            result.Total = totalCount;

            return Json(result);
        }

        /// <summary>
        /// 系统配置新增视图
        /// </summary>
        /// <returns></returns>
        [ActionName("add")]
        public ActionResult Add(int appId)
        {
            var entity = new AppConfig();
            entity.AppId = appId;
            return View("~/views/appconfig/form.cshtml", entity);
        }

        /// <summary>
        /// 系统配置编辑视图
        /// </summary>
        /// <param name="id">配置项ID(Id)</param>
        /// <returns></returns>
        [ActionName("edit")]
        public ActionResult Edit(int id)
        {
            var entity = new AppConfig();

            try
            {
                var appConfigService = this.GetService<IAppConfigService>();
                entity = appConfigService.Find<AppConfig>(id);

            }
            catch (CLApplicationException ex)
            {
                //修改时若发生异常则提示异常信息，并关闭修改界面
                this.ShowAppErrorMessage(ex.Message, MessageFuncOption.CloseBrowserWindow);
            }

            return View("~/views/appconfig/form.cshtml", entity);
        }



        /// <summary>
        /// 系统配置保存数据操作
        /// </summary>
        /// <param name="model">系统配置视图模型</param>
        /// <returns></returns>
        [ActionName("save")]
        public JsonResult Save(AppConfig model)
        {
            var appConfigService = this.GetService<IAppConfigService>();

            var id = appConfigService.SaveAppConfig(model);
            ShowMessage("I10010");
            return Json(id);
        }


        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="id">配置项ID(Id)</param>
        /// <returns>返回操作结果</returns>
        [ActionName("remove")]
        public JsonResult Remove(int[] ids)
        {
            var appConfigService = this.GetService<IAppConfigService>();

            appConfigService.LogicDeleteByIds(ids);
            ShowMessage("I10030");

            return Json(null);
        }
    }
}
