using Microsoft.AspNetCore.Mvc;
using OnlineProdajaPica.Data;
using OnlineProdajaPica.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace OnlineProdajaPica.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public List<Product> kosarica;

        public CartController(ApplicationDbContext db)
        {
            _context = db;
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

        public IActionResult AddToCart(int? id)
        {
            var productToAdd = _context.Products.Single(p => p.Id == id);
            try
            {
                kosarica = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("Cart"));
            }
            catch
            {
                kosarica = new List<Product>();
            }
            
            if (kosarica.Exists(p => p.Id == productToAdd.Id))
            {
                var productInCart = kosarica.Single(p => p.Id == productToAdd.Id);
                productInCart.Quantity += 1;
            }
            else
            {
                productToAdd.Quantity += 1;
                kosarica.Add(productToAdd);
            }
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
    }
}
