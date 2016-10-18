using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

        public async Task<ActionResult> Index()
        {
            return View(await db.Players.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        var playerStats = db.Players.Include("TeamPlayers").Include("TeamPlayers.Team").Include("TeamPlayers.Team.Results").FirstOrDefaultAsync(w => w.Id == id);
			return View(await playerStats);
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
