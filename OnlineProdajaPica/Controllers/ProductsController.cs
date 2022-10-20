using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineProdajaPica.Data;
using OnlineProdajaPica.Models;
using OnlineProdajaPica.ViewModels;

namespace OnlineProdajaPica.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext db)
        {
            _context = db;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Products
        public ActionResult Index()
        {
            var listaProizvoda = _context.Products.Include(p => p.Category).ToList();
            return View(listaProizvoda);
        }

        public ActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new ProductViewModel()
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, IFormFile userfile)
        {
            

           

            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot\\Images", filename);
            var stream = new FileStream(uploadFilePath, FileMode.Create);
            userfile.CopyToAsync(stream);
            product.ImageUrl = "/images/" + userfile.FileName;

            if (!ModelState.IsValid)
            {
                return Content("Greška");
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var productToEdit = _context.Products.Single(p => p.Id == id);
                var categories = _context.Categories.ToList();
                var viewModel = new ProductViewModel(productToEdit)
                {
                    Categories = categories
                };

                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Product updatedProduct)
        {
            try
            {
                var productToEdit = _context.Products.Single(p => p.Id == id);
                var categories = _context.Categories.ToList();
                var viewModel = new ProductViewModel(productToEdit)
                {
                    Categories = categories
                };

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                _context.Entry(productToEdit).CurrentValues.SetValues(updatedProduct);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                var productToDelete = _context.Products.Single(p => p.Id == id);
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var product = _context.Products.Include(p => p.Category).Single(p => p.Id == id);
                return View(product);
            }
            catch
            {
                return Content("Greška");
            }
        }
    }
}
