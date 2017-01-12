using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using MiniFootball.Models;
using MiniFootball.ViewModels;

namespace MiniFootball.Controllers
{
    public class GameStatsController : Controller
    {
        private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

        public async Task<ActionResult> Index(int? id)
        {
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var date = db.Results.Where(w => w.Id == id).Select(s => s.Date).FirstOrDefault();
	        var gameStats = await db.Results.Include("Team").Include("Team.TeamPlayers").Include("Team.TeamPlayers.Player").Where(w => w.Date == date).ToListAsync();
			var model = new ResultsViewModel {Results = gameStats, IsActiveGame = gameStats.Select(s => s.Team.IsTemporary == 0).FirstOrDefault()};
	        return View(model);
        }

		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var date = db.Results.Where(w => w.Id == id).Select(s => s.Date).FirstOrDefault();
			var gameStats = await db.Results.Include("Team").Include("Team.TeamPlayers").Include("Team.TeamPlayers.Player").Where(w => w.Date == date).ToListAsync();
			var model = new ResultsViewModel { Results = gameStats, IsActiveGame = gameStats.Select(s => s.Team.IsTemporary == 0).FirstOrDefault() };
			return View(model);
		}

	    [HttpPost]
	    public ActionResult Edit(ResultsViewModel model)
	    {
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
