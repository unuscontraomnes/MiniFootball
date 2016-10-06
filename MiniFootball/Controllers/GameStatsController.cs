using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class GameStatsController : Controller
    {
        private readonly ArcadiaFootballEntities db = new ArcadiaFootballEntities();

        public async Task<ActionResult> Index(int? id)
        {
			var gameStats = db.GameStats.Include(g => g.Game).Include(g => g.Player).Where(g => g.GameId == id);
			return View(await gameStats.ToListAsync());
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
