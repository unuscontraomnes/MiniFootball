using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class StatsController : Controller
    {
        private readonly ArcadiaFootballEntities db = new ArcadiaFootballEntities();

        public async Task<ActionResult> Index()
        {
			var stats = db.Stats.Include(s => s.Player).OrderByDescending(p => p.Points).ThenBy(g => g.Games).ThenByDescending(g => g.Goals);
			return View(await stats.ToListAsync());
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
