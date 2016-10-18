using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class GamesController : Controller
    {
        private readonly ArcadiaMiniFootballEntities db = new ArcadiaMiniFootballEntities();

        public async Task<ActionResult> Index()
        {
	        var games = db.Results;
	        return View(await games.ToListAsync());
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
