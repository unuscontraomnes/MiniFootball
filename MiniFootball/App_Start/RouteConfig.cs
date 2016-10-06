﻿using System.Web.Mvc;
using System.Web.Routing;

namespace MiniFootball
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.LowercaseUrls = true;

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Stats", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
