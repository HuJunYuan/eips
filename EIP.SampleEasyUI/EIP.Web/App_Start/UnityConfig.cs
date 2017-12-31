using CoreLand.Framework;
using CoreLand.Framework.Service;
using System.Data.Entity;
using System.Web;

namespace EIP.Web.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            DefaultServiceRegister.InitContainer(HttpContext.Current.Server.MapPath("Unity.config")); 
            
        }
    }
}