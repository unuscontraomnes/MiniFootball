using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MiniFootball.Models
{
	public static class Rating
	{
		public static List<Statistic> GetRating(ArcadiaMiniFootballEntities db)
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
					double monthRating;
					var index = 1 - (DateTime.Now.Month - month.Month) * 0.1;
					var teams = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0);
					var results = teams.Select(s => s.Team.Results).FirstOrDefault();
					var monthPoints = results.Where(w => w.Date.Year == month.Year && w.Date.Month == month.Month).Sum(p => p.Points);

					if (index < 0.1)
					{
						monthRating = monthPoints * 0.1;
					}
					else
					{
						monthRating = monthPoints * index;
					}

					stats.Rating = stats.Rating + monthRating;
				}

				statistic.Add(stats);
			}

			return statistic.OrderByDescending(o => o.Rating).ToList();
		}
	}
}