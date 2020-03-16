using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using aspnet2.Models;

namespace aspnet2
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {

            Database.SetInitializer(new DbInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
    