using CoreLand.Framework;
using CoreLand.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIP.Portal.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult SystemError(string msg)
        {
            string browser = Request.Browser.Browser;
            string browserVersion = Request.Browser.Version;
            string system = Request.Browser.Platform;
            string userinfo = HttpContext.User.Identity.Name;

            if (Request.Browser.Capabilities["extra"] != null)
            {
                var extra = Request.Browser.Capabilities["extra"].ToString();
                if (extra.Split(';').Any(p => p.Contains("Windows")))
                {
                    system = extra.Split(';').Where(p => p.Contains("Windows")).First().Trim();
                }
            }
            SystemErrorInfo info = new SystemErrorInfo()
            {
                Message = msg,
                Browser = browser,
                BrowserVersion = browserVersion,
                OSName = system,
                UserCode = userinfo,
                Exception = Session["SystemError"] as CLSystemException
            };
            Session["SystemError"] = null;
            return View("~/Views/Shared/Error.cshtml", info);
        }


    }
}
