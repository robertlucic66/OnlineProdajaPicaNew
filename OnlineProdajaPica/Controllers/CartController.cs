using Microsoft.AspNetCore.Mvc;
using OnlineProdajaPica.Data;
using OnlineProdajaPica.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OnlineProdajaPica.ViewModels;

namespace OnlineProdajaPica.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public List<Product> kosarica;

        public CartController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager; 
            kosarica = new List<Product>();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Cart")))
            {
                kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart")); 
                return View(kosarica);
            }
            kosarica = new List<Product>();
            return View(kosarica);
        }

        public IActionResult AddToCart(int? id, int? quantity)
        {
            if (id < 1 | id == null)
                return NotFound();
            if (quantity < 1 | quantity == null)
                return NotFound();
            var productToAdd = _context.Products.Single(p => p.Id == id);
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Cart")))
            {
                kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
            }
            else
            {
                kosarica = new List<Product>();
            }

          
            if (kosarica.Exists(p => p.Id == productToAdd.Id))
            {
                var productInCart = kosarica.Single(p => p.Id == productToAdd.Id);
                productInCart.Quantity += (int)quantity;
            }
            else
            {
                productToAdd.Quantity += (int)quantity;
                kosarica.Add(productToAdd);
            }
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(kosarica));
            return RedirectToAction("Index");
        }

        public IActionResult ChangeQuantity(int id, int quantity)
        {
            kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
            var product = kosarica.Where(p => p.Id == id).Single();
            product.Quantity = quantity;
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(kosarica));
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
                var productToRemove = kosarica.Single(p => p.Id == id);
                kosarica.Remove(productToRemove);
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(kosarica));
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Došlo je do greške");
            }           
        }

        public IActionResult AddCustomerInfo()
        {
            if(string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Cart")))
            {
                kosarica = new List<Product>();
            }
            else
            {
                kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
            }           
            if (kosarica.Count < 1)
            {
                TempData["error"] = "Nema proizvoda u košarici!";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomerInfo(CustomerInfo customerInfo)
        {
            return RedirectToAction("OrderCheck", customerInfo);
        }

        public IActionResult OrderCheck(CustomerInfo customerInfo)
        {
            if (customerInfo.Name == null)
            {
                return RedirectToAction("AddCustomerInfo");
            }
            kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));

            OrderCheckViewModel orderCheckVM = new OrderCheckViewModel()
            {
                ProductList = kosarica,
                CustomerInfo = customerInfo
            };
            HttpContext.Session.SetString("CustomerInfo", JsonConvert.SerializeObject(customerInfo));

            return View(orderCheckVM);
        }

        public IActionResult SendOrder(CustomerInfo customerInfo)
        {
            kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
            List<int> productList = new List<int>();
            List<int> quantityList = new List<int>();
            foreach (var item in kosarica)
            {
                productList.Add(item.Id);
                quantityList.Add(item.Quantity);
            }
            Order order = new Order()
            {
                Proizvodi = String.Join(",", productList),
                Kolicine = String.Join(",", quantityList),
                UserId = _userManager.GetUserId(HttpContext.User),
                DatumNarudzbe = DateTime.Now,
                Dostavljeno = false
            };

            customerInfo.UserId = order.UserId;
            customerInfo.OrderId = order.Id;
            _context.customerInfos.Add(customerInfo);
            _context.Orders.Add(order);
            _context.SaveChanges();
            HttpContext.Session.SetString("Cart", String.Empty);
            return RedirectToAction("Index");
        }
    }
}
