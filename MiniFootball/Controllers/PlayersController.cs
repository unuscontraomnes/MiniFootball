using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
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

	    public ActionResult Create()
	    {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Player player)
		{
			if (ModelState.IsValid)
			{
				db.Players.Add(player);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}

		public async Task<ActionResult> Edit(int? id)
	    {
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var player = db.Players.Include("TeamPlayers").Include("TeamPlayers.Team").Include("TeamPlayers.Team.Results").FirstOrDefaultAsync(w => w.Id == id);
			return View(await player);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Player player)
		{
			if (ModelState.IsValid)
			{
				db.Entry(player).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Details");
			}
			return View(player);
		}

		[HttpPost]
		public ActionResult Delete(int? id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var player = db.Players.Find(id);
			db.Players.Remove(player);
			db.SaveChanges();
			return RedirectToAction("Index");
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
