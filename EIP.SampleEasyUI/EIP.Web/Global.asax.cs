using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CoreLand.Framework.Log;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Microsoft.Practices.Unity.Mvc;

namespace EIP.Web.WebUI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 错误信息配置导入
            CoreLand.Framework.Message.MessageManager.Load(Server.MapPath("~/bin/Message/Message.xml"));
            CoreLand.Framework.Message.MessageManager.Load(Server.MapPath("~/App_Data/Message.config"));

            // 依赖注入配置导入
            UnityConfig.RegisterComponents();

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // AutoMapper映射配置导入
            EIP.Service.AutoMapper.Configuration.Configure();

        }

    }
}