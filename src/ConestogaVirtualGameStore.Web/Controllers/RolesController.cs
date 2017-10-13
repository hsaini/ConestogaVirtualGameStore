using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ConestogaVirtualGameStore.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConestogaVirtualGameStore.Web.Models;
using Microsoft.AspNetCore.Http;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> RoleManager;

        public RolesController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            RoleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _context.Roles.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection collection)

        {
            try
            {
                _context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                await _context.SaveChangesAsync();
                ViewBag.RoleCreated = "Role Successfully Created";
                return RedirectToAction("Index","Roles");
            } catch
            {
                return View();
            }
        }
    }
}