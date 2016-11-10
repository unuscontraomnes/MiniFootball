using System;
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
				var dates = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(tp => tp.Team.Results.Select(s => s.Date).FirstOrDefault()).ToList();
				var months = dates.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().OrderByDescending(y => y.Year).ThenByDescending(m => m.Month).ToList();

				var stats = new Statistic
				{
					Name = player.Name,
					LastName = player.LastName,
					Games = player.TeamPlayers.Count(w => w.Team.IsTemporary == 0),
					Points = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(tp => tp.Team.Results.Sum(r => r.Points)).Sum(p => p),
					Rating = 0
				};

				foreach (var month in months)
				{
					decimal monthRating;
					var index = (decimal) (1 - Math.Abs((DateTime.Now.Month - month.Month) + 12 * (DateTime.Now.Year - month.Year)) * 0.1);
					var teams = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).ToList();
					var results = teams.Select(s => s.Team.Results.FirstOrDefault());
					var monthResults = results.Where(w => w.Date.Year == month.Year && w.Date.Month == month.Month).ToList();
					var monthPoints = monthResults.Sum(p => p.Points);

					if (index < (decimal) 0.1)
					{
						monthRating = (decimal)monthPoints / monthResults.Count * (decimal) 0.1;
					}
					else
					{
						monthRating = (decimal)monthPoints / monthResults.Count * index;
					}
					
					stats.Rating = stats.Rating + monthRating;
				}

				stats.Rating = Math.Round(stats.Rating, 2, MidpointRounding.AwayFromZero);
				statistic.Add(stats);
	        }

	        var sortedStatistic =  statistic.OrderByDescending(o => o.Rating).ThenByDescending(t => t.Points).ThenBy(d => d.Games);

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
