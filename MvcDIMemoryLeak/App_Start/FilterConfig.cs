using System.Web;
using System.Web.Mvc;

namespace MvcDIMemoryLeak
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
