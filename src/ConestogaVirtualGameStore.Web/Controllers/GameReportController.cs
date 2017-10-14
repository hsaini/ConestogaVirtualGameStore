using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConestogaVirtualGameStore.Web.Data;
using ConestogaVirtualGameStore.Web.Models;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    public class GameReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameReport
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.ToListAsync());
        }
    }
}
