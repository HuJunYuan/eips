using System.Web;
using System.Web.Mvc;
using CoreLand.Framework.Web.Attribute;

namespace EIP.Web.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CLActionFilterAttribute());
            filters.Add(new CLExceptionFilterAttribute());
        }
    }
}