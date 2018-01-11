using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EncompWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

			//config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = "all" }
			);


			
			config.Routes.MapHttpRoute(
				name: "DefaultApi0",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = "1" }
			);


			config.Routes.MapHttpRoute(
					name: "DefaultApi1",
					routeTemplate: "api/{controller}/{action}"
					, defaults: new { Guid = "all" } //  = RouteParameter.Optional }
					//, defaults: new { loanGuid  = RouteParameter.Optional } //  }
					 //defaults: new { id = RouteParameter.Optional } // if name (id here) doesn't match, then it is ignored (
				   );

			config.Routes.MapHttpRoute(
					name: "DefaultApi2",
					routeTemplate: "api/{controller}/{action}/{loanGuid}"
					, defaults: new { loanGuid = "all" } //  = RouteParameter.Optional }
					//, defaults: new { loanGuid  = RouteParameter.Optional } //  }
					 //defaults: new { id = RouteParameter.Optional } // if name (id here) doesn't match, then it is ignored (
				   );

			config.Routes.MapHttpRoute(
					name: "DefaultApi3",
					// http://localhost/PennyMac/api/Test/TestService/blah/jumbo/999
					routeTemplate: "api/{controller}/{action}/{category}/jumbo/{loanGuid}"
					, defaults: new { category = "cat", loanGuid = RouteParameter.Optional }
				   // , defaults: new { category = "all", loanGuid = RouteParameter.Optional } //  = RouteParameter.Optional }
				   //defaults: new { id = RouteParameter.Optional } // if name (id here) doesn't match, then it is ignored (
				   );
		}
	}
}
