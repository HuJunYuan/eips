/*-----------------------------------------------------------------------------
  版 本 号：V1.0 Copyright (c) Coreland.com.  All Rights Reserved.
  创建时间：2017-08-14 20:25:09   创建人：杨杰
  修改时间：                     修改人：          修改内容：
  描    述：
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Drawing;
using CoreLand.Framework;
using CoreLand.Framework.Service;
using CoreLand.Framework.Web;
using CoreLand.Framework.Models;
using Newtonsoft.Json;
using EIP.Model;

namespace EIP.Web.Web.Controllers
{
    public class IconSelectController : BaseController
    {
        ///// <summary>
        ///// 图标选择
        ///// </summary>
        ///// <returns></returns>
        //[ActionName("iconselect")]
        //public ActionResult Iconselect()
        //{
        //    string path = Server.MapPath(@"~/Scripts/jquery-easyui-1.5/themes/Icons.json");
        //    string JsonData = "";
        //    List<IconViewModel> IcoList = new List<IconViewModel>();
        //    if (System.IO.File.Exists(path))
        //    {
        //        JsonData = System.IO.File.ReadAllText(path);
        //        JsonData = JsonData.Replace(Environment.NewLine, "");//清除换行符
        //        IcoList = JsonConvert.DeserializeObject<List<IconViewModel>>(JsonData);
        //    }
        //    return View(IcoList);
        //}
    }
}
