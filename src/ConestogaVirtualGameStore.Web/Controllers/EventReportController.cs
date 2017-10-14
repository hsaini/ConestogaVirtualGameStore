namespace ConestogaVirtualGameStore.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;

    public class EventReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventReportController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: EventReport
        public async Task<IActionResult> Index()
        {
            return View(await this._context.Events.ToListAsync());
        }
    }
}
