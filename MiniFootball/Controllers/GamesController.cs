using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using MiniFootball.Models;

namespace MiniFootball.Controllers
{
    public class GamesController : Controller
    {
        private readonly ArcadiaFootballEntities db = new ArcadiaFootballEntities();

        public async Task<ActionResult> Index()
        {
            return View(await db.Games.ToListAsync());
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
