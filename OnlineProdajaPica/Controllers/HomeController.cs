using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineProdajaPica.Models;
using System.Diagnostics;
using System.Drawing.Text;

namespace OnlineProdajaPica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            //this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
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