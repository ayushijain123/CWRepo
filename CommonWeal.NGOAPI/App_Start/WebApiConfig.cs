using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;


namespace CommonWeal.NGOAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {//enable CORS - cross origin request

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            //config.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
            config.Formatters.Add(new System.Net.Http.Formatting.FormUrlEncodedMediaTypeFormatter());




        }


    }
}
