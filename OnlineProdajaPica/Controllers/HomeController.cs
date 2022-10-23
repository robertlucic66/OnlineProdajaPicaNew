using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineProdajaPica.Data;
using OnlineProdajaPica.Models;
using System.Diagnostics;
using System.Drawing.Text;

namespace OnlineProdajaPica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        //private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _context = db;
            _logger = logger;
            //this.roleManager = roleManager;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        // dodan Admin Role
        /*
        public async Task<IActionResult> CreateRole()
        {
            var roleExist = await roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            return RedirectToAction("Privacy");
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}