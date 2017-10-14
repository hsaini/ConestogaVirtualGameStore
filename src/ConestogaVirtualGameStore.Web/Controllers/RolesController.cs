using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ConestogaVirtualGameStore.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ConestogaVirtualGameStore.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ConestogaVirtualGameStore.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> RoleManager;
        private UserManager<ApplicationUser> UserManager;

        public RolesController(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            RoleManager = roleManager;
            UserManager = userManager;

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
                return RedirectToAction("Index", "Roles");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(string roleName)
        {
            var role = _context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ManageUsers()
        {
            var roleList = _context.Roles.OrderBy(b => b.Name).ToList()
                .Select(bb => new SelectListItem { Value = bb.Name.ToString(), Text = bb.Name }).ToList();


            ViewBag.Roles = roleList;
            return View("ManageUsers");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string UserName, string RoleName)
        {
            ApplicationUser user = _context.Users.Where(a => a.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            UserManager.AddToRoleAsync(user, RoleName);

            ViewBag.Results = UserName + "has been added to the " + RoleName + "group";


            var roleList = _context.Roles.OrderBy(b => b.Name).ToList()
               .Select(bb => new SelectListItem { Value = bb.Name.ToString(), Text = bb.Name }).ToList();
            ViewBag.Roles = roleList;

            return RedirectToAction("Index");

        }
    }
}