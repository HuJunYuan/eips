using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreLand.Framework;
using CoreLand.Framework.Authentication;
using CoreLand.Framework.Attributes;
using System.Text;
using CoreLand.Framework.Web;
using CoreLand.Framework.Message;
using EIP.Model;
using EIP.Entity;
using EIP.Service;
using System.Drawing;
using EIP.Portal.Model;
using EIP.Portal.Entity;
using EIP.Portal.Service;
namespace EIP.Web.Controllers
{
    public class LoginController : Controller
    {
        protected MessageInfoView MessageInfoView { get; private set; }

        public ActionResult Index()
        {

            return View("~/Views/Home/login.cshtml");
        }

        /// <summary>
        /// 默认跳转
        /// </summary>
        /// <returns></returns>
         [ActionName("home")]
        public ActionResult Home()
        {

            return View("~/Views/Home/Index.cshtml");
        }


        [OperationLog("用户登录")]
        public JsonResult Login(string userName, string password)
        {
            var authService = AppContext.ServiceFactory.CreateService<IAuthenticationService>();
            var userInfo = authService.ValidateUser(userName, password);
            if (userInfo == null)
            {
                throw new CLApplicationException("E01000");
            }
            authService.SignIn(userInfo, true);
            //this.ShowMessage("I10000", "window.location.href = '/home';");

            return Json("Success");
        }

        [OperationLog("用户注销")]
        public JsonResult Logout()
        {
            var authService = AppContext.ServiceFactory.CreateService<IAuthenticationService>();
            authService.SignOut();
            this.ShowMessage("I10001");
            return Json(null);
        }

        /// <summary>
        /// 修改密码视图
        /// </summary>
        /// <param name="LogName"></param>
        /// <returns></returns> 
        [ActionName("editPassword")]
        public ActionResult EditPassword(string logName)
        {
            var uUCEmployeeService = AppContext.ServiceFactory.CreateService<IUUCEmployeeService>();
            var entiry = new MailEditPasswordViewModel();
            entiry = uUCEmployeeService.GetLogName(logName);
            return View("~/views/Login/MailEditPassword.cshtml", entiry);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        [OperationLog("用户修改密码")]
        [ActionName("savePassword")]
        public JsonResult SavePassword(MailEditPasswordViewModel model)
        {
            var uUCEmployeeService = AppContext.ServiceFactory.CreateService<IUUCEmployeeService>();
            uUCEmployeeService.SavPasswordByUser(model);
            //保存成功
            ShowMessage("I10010");
            return Json(true);
        }

        /// <summary>
        /// 设置和发送验证码
        /// </summary>
        /// <param name="LogName"></param>
        /// <param name="Mail"></param>
        /// <returns></returns>
        [ActionName("sendAuth")]
        [OperationLog("发送修改密码验证码")]
        public JsonResult SendAuth(string logName, string email)
        {
            var uUCEmployeeService = AppContext.ServiceFactory.CreateService<IUUCEmployeeService>();
            uUCEmployeeService.SendAuth(logName, email);
            ShowMessage("I10060");
            return Json(true);
        }

        #region 信息处理方法
        protected void ShowMessage(string messageCode, params object[] msgParams)
        {
            this.ShowMessage(messageCode, MessageFuncOption.None, string.Empty, msgParams);
        }
        protected void ShowMessage(string messageCode, MessageFuncOption option, params object[] msgParams)
        {
            this.ShowMessage(messageCode, option, string.Empty, msgParams);
        }
        protected void ShowMessage(string messageCode, string callBackScript, params object[] msgParams)
        {
            this.ShowMessage(messageCode, MessageFuncOption.None, callBackScript, msgParams);
        }
        protected void ShowMessage(string messageCode, MessageFuncOption option, string callBackScript, params object[] msgParams)
        {
            int icon = 0;
            string message = MessageManager.GetMessage(messageCode, msgParams);
            MessageInfo messageInfo = MessageManager.GetMessageInfo(messageCode);
            switch (messageInfo.Type)
            {
                case MessageType.ERROR:
                    icon = 2;
                    break;
                case MessageType.INFO:
                    icon = 1;
                    break;
                case MessageType.QUESTION:
                    icon = 3;
                    break;
                case MessageType.WARN:
                    icon = 0;
                    break;
                default:
                    icon = 1;
                    break;

            }
            this.MessageInfoView = new MessageInfoView(message, icon, option, callBackScript);
        }

        protected void ShowAppErrorMessage(string message)
        {
            this.MessageInfoView = new MessageInfoView(message, 2, MessageFuncOption.None, string.Empty);
        }

        protected void ShowAppErrorMessage(string message, MessageFuncOption option)
        {
            this.MessageInfoView = new MessageInfoView(message, 2, option, string.Empty);
        }
        #endregion

        #region 返回结果封装
        protected override System.Web.Mvc.ViewResult View(string viewName, string masterName, object model)
        {
            ViewBag.MessageInfoView = this.MessageInfoView;
            return base.View(viewName, masterName, model);
        }

        protected override System.Web.Mvc.ViewResult View(System.Web.Mvc.IView view, object model)
        {
            ViewBag.MessageInfoView = this.MessageInfoView;
            return base.View(view, model);
        }

        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return base.Json(FormatJsonData(data), contentType, contentEncoding);
        }

        protected override System.Web.Mvc.JsonResult Json(object data, string contentType, Encoding contentEncoding, System.Web.Mvc.JsonRequestBehavior behavior)
        {
            // return base.Json(FormatJsonData(data), contentType, contentEncoding, behavior);

            return new JsonResultExtensions()
            {
                Data = FormatJsonData(data),
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected override System.Web.Mvc.PartialViewResult PartialView(string viewName, object model)
        {
            ViewBag.MessageInfoView = this.MessageInfoView;
            return base.PartialView(viewName, model);
        }

        private JsonObject FormatJsonData(object data)
        {
            var jsonData = new JsonObject()
            {
                Success = true,
                Data = data
            };
            if (this.MessageInfoView != null)
            {
                jsonData.Success = this.MessageInfoView.MessageIcon != 2;
                jsonData.Icon = this.MessageInfoView.MessageIcon;
                jsonData.Message = this.MessageInfoView.Message;
                jsonData.CallBackScript = this.MessageInfoView.CallBackScriptFunc;
            }
            return jsonData;
        }
        #endregion

    }
}
