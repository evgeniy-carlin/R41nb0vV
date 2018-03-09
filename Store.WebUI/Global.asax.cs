using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Store.Domain.Concrete;
using Store.Domain.Entities;
using Store.WebUI.Infrastructures.Binders;

namespace Store.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            Database.SetInitializer<EFDbContext>(null);
        }
    }
}
