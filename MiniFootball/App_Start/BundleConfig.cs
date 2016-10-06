using System.Web;
using System.Web.Optimization;

namespace MiniFootball
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
						"~/Scripts/jquery-ui.js"));

			bundles.Add(new ScriptBundle("~/bundles/helper").Include(
						"~/Scripts/helper.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/jquery-ui.css",
					  "~/Content/jquery-ui.min.css",
					  "~/Content/jquery-ui.structure.css",
					  "~/Content/jquery-ui.structure.min.css",
					  "~/Content/jquery-ui.theme.css",
					  "~/Content/jquery-ui.theme.min.css"));
		}
	}
}
