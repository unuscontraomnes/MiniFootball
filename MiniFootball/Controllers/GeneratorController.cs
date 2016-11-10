using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class GeneratorController : Controller
    {
		private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

		public async Task<ActionResult> Index()
		{
			return View(await db.Players.ToListAsync());
		}

		public ActionResult Autocomplete(string term)
		{
			var filteredItems = db.Players.Where(
				item => item.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0 || item.LastName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
				);
			return Json(filteredItems, JsonRequestBehavior.AllowGet);
		}

		//public JsonResult GetMatchedCities(string q)
		//{
		//	return this.Json(query, JsonRequestBehavior.AllowGet);
		//}
	}
}