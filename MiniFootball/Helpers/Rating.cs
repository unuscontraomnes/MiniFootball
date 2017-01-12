using System;
using System.Collections.Generic;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Helpers
{
	public static class Rating
	{
		public static List<Statistic> GetRating(List<Player> players)
		{
			var statistic = new List<Statistic>();

			foreach (var player in players)
			{
				var stats = new Statistic
				{
					Id = player.Id,
					Name = player.Name,
					LastName = player.LastName,
					Games = 0,
					Points = 0,
					Rating = 0
				};

				if (!player.TeamPlayers.Any())
				{
					statistic.Add(stats);
					continue;
				}

				var dates = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(tp => tp.Team.Results.Select(s => s.Date).FirstOrDefault()).ToList();
				var months = dates.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().OrderByDescending(y => y.Year).ThenByDescending(m => m.Month).ToList();

				foreach (var month in months)
				{
					decimal monthRating;

					var index = (decimal)(1 - Math.Abs((DateTime.Now.Month - month.Month) + 12 * (DateTime.Now.Year - month.Year)) * 0.1);

					var monthResults = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0)
					    .Select(s => s.Team.Results.FirstOrDefault())
					    .Where(w => w.Date.Year == month.Year && w.Date.Month == month.Month).ToList();

					var monthPoints = monthResults.Sum(p => p.Points);

					if (index < (decimal)0.1)
					{
						monthRating = (decimal)monthPoints / monthResults.Count * (decimal)0.1;
					}
					else
					{
						monthRating = (decimal)monthPoints / monthResults.Count * index;
					}
					
					stats.Rating = stats.Rating + monthRating;
				}

				stats.Games = player.TeamPlayers.Count(w => w.Team.IsTemporary == 0);
				stats.Points = player.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(tp => tp.Team.Results.Sum(r => r.Points)).Sum(p => p);
				stats.Rating = Math.Round(stats.Rating, 2, MidpointRounding.AwayFromZero);
				statistic.Add(stats);
			}

			return statistic.OrderByDescending(o => o.Rating).ThenByDescending(t => t.Points).ThenBy(d => d.Games).ToList();
		}
	}
}