using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XX.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //重写testIndex.html
            routes.MapRoute(
                name: "testshow",
                url: "testShow.html",
                defaults: new {controller = "Test", action = "Show", id = UrlParameter.Optional});
            //以下三种 只能使用一种
            //重写分页形式1-目录形式
            routes.MapRoute(
                name: "Test_Index",
                url:"test-{key}-{page}",
                defaults: new {controller = "Test", action = "Index",pagesize=10},
                constraints: new { key = @"([\s\S]*)", page = @"[\d]*" }
                );
            ////重写分页形式2-html形式
            //routes.MapRoute(
            //    name: "Test_Index",
            //    url: "test-{key}-{page}.html",
            //    defaults: new { controller = "Test", action = "Index",pagesize=10 },
            //    constraints: new { key = @"([\s\S]*)", page = @"[\d]*" }
            //    );
            //////重写分页形式3-目录形式
            //routes.MapRoute(
            //    name: "Test_Index",
            //    url: "test/{key}/{page}.html",
            //    defaults: new { controller = "Test", action = "Index" ,pagesize=10},
            //    constraints: new { key = @"([\s\S]*)", page = @"[\d]*" }
            //    );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
