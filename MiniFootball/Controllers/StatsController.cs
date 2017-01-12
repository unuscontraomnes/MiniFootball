using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using MiniFootball.Helpers;
using MiniFootball.Models;
using MiniFootball.ViewModels;

namespace MiniFootball.Controllers
{
    public class StatsController : Controller
    {
        private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

        public ActionResult Index()
        {
			var playerIdList = db.TeamPlayers.Where(w => w.Team.IsTemporary == 0).Select(s => s.PlayerId).Distinct().ToList();
			var players = db.TeamPlayers.Include(i => i.Player).Include(i => i.Team).Include(i => i.Team.Results).Select(s => s.Player).Distinct().Where(w => playerIdList.Contains(w.Id)).ToList();
			var model = new StatsViewModel {Statistics = Rating.GetRating(players) };
			return View(model);
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
