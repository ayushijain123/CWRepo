using System.Web;
using System.Web.Http;
 

namespace CommonWeal.NGOAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration config)
        {

            config.Filters.Add(new System.Web.Http.AuthorizeAttribute() { Roles = "Admin,User,NGOUser" });

            //filters.Add(new RequireHttpsAttribute());
        }
    }
}