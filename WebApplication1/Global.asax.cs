using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //PythonEngine.Initialize();
        }
        //protected void Application_End()
        //{
        //    // Application shutdown logic
        //    PythonEngine.Shutdown();
        //}
    }
}
