using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace TextIOWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();


			// Any specific routes first... then finally the "default"...
			config.Routes.MapHttpRoute(
                name: "TextIOSpecific",
                routeTemplate: "api/{controller}/{action}/{id}"
				// Remove below because once it is optional then the "default" pattern below will never be reached
                // defaults: new { id = RouteParameter.Optional } 
            );


			// Default w/ Action (no params)..
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithAction0",
                routeTemplate: "api/{controller}/{action}"  
                //,defaults: new { id = RouteParameter.Optional }
            );

			// Default..
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",  // api/values, api/values/1
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
