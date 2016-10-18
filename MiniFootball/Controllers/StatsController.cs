using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class StatsController : Controller
    {
        private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

        public ActionResult Index()
        {
			var playerIdList = db.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(s => s.PlayerId).Distinct().ToList();
			var players = db.TeamPlayers.Include(i => i.Player).Include(i => i.Team).Include(i => i.Team.Results).Select(s => s.Player).Distinct().Where(w => playerIdList.Contains(w.Id));
			var statistic = new List<Statistic>();

			foreach (var player in players)
	        {
		        var stats = new Statistic
		        {
			        Name = player.Name,
					LastName = player.LastName,
					Games = player.TeamPlayers.Count(w => w.Team.IsTemporary == 0),
					Points = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(tp => tp.Team.Results.Sum(r => r.Points)).Sum(p => p)
				};
				statistic.Add(stats);
	        }

	        var sortedStatistic =  statistic.OrderByDescending(o => o.Points).ThenBy(t => t.Games);

			return View(sortedStatistic);
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
