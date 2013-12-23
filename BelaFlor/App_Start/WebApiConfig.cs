using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace BelaFlor
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "GetArticlesCategory",
               routeTemplate: "api/{controller}/category/{catid}"
           );

            config.Routes.MapHttpRoute(
              name: "GetImageMethod1",
              routeTemplate: "api/{controller}/{imgid}/image/{width}/{height}"
           );

            config.Routes.MapHttpRoute(
              name: "GetImageMethod2",
              routeTemplate: "api/{controller}/{imgid}/image"
           );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "PostMethod",
               routeTemplate: "api/{controller}/create"
           );

            config.Routes.MapHttpRoute(
               name: "PutMethod",
               routeTemplate: "api/{controller}/update"
           );

            config.Routes.MapHttpRoute(
               name: "ActivateMethod",
               routeTemplate: "api/{controller}/activate"
           );


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api

            config.EnableSystemDiagnosticsTracing();
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter());
            //config.Formatters.Add(new JQueryMvcFormUrlEncodedFormatter());
        }
    }
}
