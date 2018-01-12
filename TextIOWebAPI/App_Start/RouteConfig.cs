using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TextIOWebAPI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// Any specific routes first... then finally the "default"...


			// Default..
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}", // MVC routing
				// defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // default generated
				defaults: new { id = UrlParameter.Optional }
			);
		}
	}
}
