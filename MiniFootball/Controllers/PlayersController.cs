using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ArcadiaFootballEntities db = new ArcadiaFootballEntities();

        public async Task<ActionResult> Index()
        {
            return View(await db.Players.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var stats = db.Stats.Where(p => p.PlayerId == id).Include(s => s.Player).FirstOrDefaultAsync();
			if (stats == null)
            {
                return HttpNotFound();
            }
			return View(await stats);
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
