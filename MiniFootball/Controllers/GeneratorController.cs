using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MiniFootball.Helpers;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class GeneratorController : Controller
    {
		private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

		public ActionResult Index()
		{
			var playerIdList = db.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(s => s.PlayerId).Distinct().ToList();
			var players = db.TeamPlayers.Include(i => i.Player).Include(i => i.Team).Include(i => i.Team.Results).Select(s => s.Player).Distinct().Where(w => playerIdList.Contains(w.Id));

			return View(Rating.GetRating(players));
		}

		[HttpPost]
		public ActionResult Index(string playersId)
		{
			if (string.IsNullOrEmpty(playersId)) return RedirectToAction("Index");

			var idList = playersId.Split(',', '-', ' ', '.', ';', ':').Select(int.Parse).Distinct().Where(w => w != 0).ToList();
			var players = db.TeamPlayers.Include(i => i.Player).Include(i => i.Team).Include(i => i.Team.Results).Select(s => s.Player).Distinct().Where(w => idList.Contains(w.Id));
			var teams = TeamGenerator.Generate(Rating.GetRating(players));

			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}