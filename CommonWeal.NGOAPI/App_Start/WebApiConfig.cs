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
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        //config.Formatters.Clear();
        //config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
        ////config.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
        //config.Formatters.Add(new System.Net.Http.Formatting.FormUrlEncodedMediaTypeFormatter());

    }
}
