using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CLMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes


                config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            FormUrlEncodedMediaTypeFormatter f;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            f = new FormUrlEncodedMediaTypeFormatter();
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
            f.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-www-form-urlencoded"));

            config.Formatters.Add(f);
        }
    }
}
