using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.Security.OAuth;


namespace CommonWeal.NGOAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{controller}/{action}/{id}",
                //defaults: new { id = RouteParameter.Optional }
                routeTemplate: "api/{controller}/{action}"
                
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌ferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        //config.Formatters.Clear();
        //config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        ////config.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
        //config.Formatters.Add(new System.Net.Http.Formatting.FormUrlEncodedMediaTypeFormatter());

    }
}
