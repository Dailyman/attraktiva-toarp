using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using EventHandlingSystem;

namespace EventHandlingSystem
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        private void Application_Error(object sender, EventArgs e)
        {
            string filePath = HttpContext.Current.Server.MapPath("~") + "/Admin/ErrorPage.html";
            Exception exception = Server.GetLastError();
            string errorPage = 
                System.IO.File.ReadAllText
                (filePath);

            if (exception is HttpRequestValidationException)
            {
                Response.Clear();
                Response.StatusCode = 200;
                Response.Write(errorPage);
                Response.End();
            }
        }
    }
}
