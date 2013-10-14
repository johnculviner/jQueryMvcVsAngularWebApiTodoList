using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using jQueryMvcVsAngularWebApiTodoList.WebApiAngular.Filters;

namespace jQueryMvcVsAngularWebApiTodoList
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //
            //WebAPI setup
            //
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            //
            //MVC setup
            //
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("");
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "MvcJquery/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
            //custom path for views for clean separation of webapi/mvc
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomViewEngine());
        }
    }

    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var viewLocations = new[] {  
               "~/MvcjQuery/Views/{0}.cshtml",
            };

            PartialViewLocationFormats = viewLocations;
            ViewLocationFormats = viewLocations;
        }
    }
}