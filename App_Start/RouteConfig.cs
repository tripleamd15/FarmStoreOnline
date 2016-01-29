using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FarmStore0906
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(null,  //1
            //   "",
            //  new
            //  {
            //      controller = "Fruit",
            //      action = "Show",
            //      category = (string)null,
            //      page = 1
            //  }
            //);

            routes.MapRoute(null,  //1
               "",
              new
              {
                  controller = "Fruit",
                  action = "Introduction",
                  category = (string)null,
                  page = 1
              }
            );

            routes.MapRoute(null,  //2
              "Page{page}",
             new { controller = "Fruit", action = "Show", category = (string)null },
             new { page = @"\d+" }
           );

            routes.MapRoute(  //3
              null,
              "{category}",  //{category } causes problem!!!!!!!!!!
             new { controller = "Fruit", action = "Show", page = 1 }
           );

            routes.MapRoute(  //4
              null, "{category}/Page{page}",
             new
             {
                 controller = "Fruit",
                 action = "Show"
             },
             new { page = @"\d+" }
           );

            routes.MapRoute(null, "{controller}/{action}");          
       
        }
    }
}
