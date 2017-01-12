using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MiniFootball.Helpers;
using MiniFootball.Models;
using MiniFootball.ViewModels;

namespace MiniFootball.Controllers
{
    public class GeneratorController : Controller
    {
		private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

		public async Task<ActionResult> Index()
		{
			var players = await db.Players.OrderBy(o => o.LastName).ToListAsync();
			var model = new GeneratorViewModel
			{
				CheckedPlayers = players.Select(player => new CheckedPlayers {Id = player.Id, Name = player.LastName + " " + player.Name}).ToList()
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult Index(GeneratorViewModel model)
		{
			if (!model.CheckedPlayers.Any() || model.CheckedPlayers.Count < 2) return RedirectToAction("Index");
			var checkedIds = model.CheckedPlayers.Where(w => w.IsChecked).Select(s => s.Id).ToList();
			var players = db.Players.Include("TeamPlayers").Include("TeamPlayers.Team").Include("TeamPlayers.Team.Results").Where(w => checkedIds.Contains(w.Id)).ToList();
			var teams = TeamGenerator.Generate(Rating.GetRating(players));

			var teamIds = new List<int>();
			for (var i = 1; i < teams.Count + 1; i++)
			{
				var team = new Team {IsTemporary = 1};
				db.Teams.Add(team);
				db.SaveChanges();
				teamIds.Add(team.Id);

				foreach (var statistic in teams[i])
				{
					var teamPlayer = new TeamPlayer {TeamId = team.Id, PlayerId = statistic.Id};
					db.TeamPlayers.Add(teamPlayer);
					db.SaveChanges();
				}
			}

			var resultIds = new List<int>();
			foreach (var teamId in teamIds)
			{
				var result = new Result {TeamId = teamId, Points = 0, Date = DateTime.Now.Date};
				db.Results.Add(result);
				db.SaveChanges();
				resultIds.Add(result.Id);
			}

			return Redirect($"/gamestats/{resultIds.FirstOrDefault()}");
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