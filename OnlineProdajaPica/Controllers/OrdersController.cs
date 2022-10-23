using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineProdajaPica.Data;
using OnlineProdajaPica.Models;
using OnlineProdajaPica.ViewModels;
using System.Data;

namespace OnlineProdajaPica.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager; 
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public IActionResult Index()
        
        {
            List<OrdersViewModel> listOfOrders = new List<OrdersViewModel>();

            var orders = _context.Orders.ToList();
            foreach (var item in orders)
            {

                string[] nizProducta = item.Proizvodi.Split(',');
                string[] nizKolicina = item.Kolicine.Split(',');
                List<int> nizProductaInt = new List<int>();
                List<int> nizKolicinaInt = new List<int>();
                foreach (var product in nizProducta)
                {
                    nizProductaInt.Add(Int32.Parse(product));
                }
                foreach (var kolicina in nizKolicina)
                {
                    nizKolicinaInt.Add(Int32.Parse(kolicina));
                }
                List<Product> listaProizvoda = new List<Product>();
                foreach (var idProducta in nizProductaInt)
                {
                    var index = nizProductaInt.IndexOf(idProducta);
                    var product = _context.Products.Single(p => p.Id == idProducta);
                    var _product = new Product()
                    {
                        Id = product.Id,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        ImageUrl = product.ImageUrl,
                        Name = product.Name,
                        NumberInStock = product.NumberInStock,
                        Quantity = nizKolicinaInt[index]
                    };

                    listaProizvoda.Add(_product);
                }

                OrdersViewModel ordersVM = new OrdersViewModel(item)
                {
                    Username = _userManager.Users.Single(u => u.Id == item.UserId).UserName,
                    Products = listaProizvoda
                };

                listOfOrders.Add(ordersVM);
            }

            return View(listOfOrders.OrderByDescending(o => o.DatumNarudzbe));
        }

        public ActionResult Details(int? id)
        {
            if (id < 1)
            {
                return Content("Nesipravan ID");
            }
            try
            {
                var order = _context.Orders.Single(o => o.Id == id);
                string[] nizProducta = order.Proizvodi.Split(',');
                string[] nizKolicina = order.Kolicine.Split(',');
                List<int> nizProductaInt = new List<int>();
                List<int> nizKolicinaInt = new List<int>();
                foreach (var product in nizProducta)
                {
                    nizProductaInt.Add(Int32.Parse(product));
                }
                foreach (var kolicina in nizKolicina)
                {
                    nizKolicinaInt.Add(Int32.Parse(kolicina));
                }
                List<Product> productList = new List<Product>();
                foreach (var item in nizProductaInt)
                {
                    var product = _context.Products.Single(p => p.Id == item);
                    var index = nizProductaInt.IndexOf(item);
                    product.Quantity = nizKolicinaInt[index];
                    productList.Add(product);
                }

                OrdersViewModel orderVM = new OrdersViewModel(order)
                {
                    Products = productList,
                    Username = _userManager.Users.Single(u => u.Id == order.UserId).UserName
                };
                ViewBag.UserId = order.UserId;

                return View(orderVM);
            }
            catch
            {
                return Content("Došlo je do greške");
            }
        }

        public IActionResult MarkAsDelivered(int? id)
        {
            if (id < 1 | id == null)
            {
                return Content("Neispravan ID");
            }

            var order = _context.Orders.Single(p => p.Id == id);
            order.Dostavljeno = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
