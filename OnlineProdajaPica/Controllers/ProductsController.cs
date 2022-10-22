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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(p=>p.Category).ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            var viewModel = new ProductViewModel()
            {
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile userfile)
        {
            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot\\Images", filename);
            var stream = new FileStream(uploadFilePath, FileMode.Create);
            await userfile.CopyToAsync(stream);
            product.ImageUrl = "/images/" + userfile.FileName;

            if (!ModelState.IsValid)
            {
                return Content("Greška");
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            TempData["success"] = "Uspješno kreiran proizvod!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id < 1 || id == null)
            {
                return NotFound();
            }
            try
            {
                var productToEdit = await _context.Products.FindAsync(id);
                var categories = _context.Categories.ToList();
                if(productToEdit == null)
                {
                    return NotFound();
                }
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
        public ActionResult Edit(int id, Product updatedProduct, IFormFile? userfile)
        {
            try
            {
                var productToEdit = _context.Products.Single(p => p.Id == id);
                var categories = _context.Categories.ToList();
                var viewModel = new ProductViewModel(productToEdit)
                {
                    Categories = categories
                };
                if (userfile != null)
                {
                    string filename = userfile.FileName;
                    filename = Path.GetFileName(filename);
                    string uploadFilePath = Path.Combine
                        (Directory.GetCurrentDirectory(), "wwwroot\\Images", filename);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    userfile.CopyToAsync(stream);
                    updatedProduct.ImageUrl = "/images/" + userfile.FileName;
                }
                else
                {
                    updatedProduct.ImageUrl = productToEdit.ImageUrl;
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                _context.Entry(productToEdit).CurrentValues.SetValues(updatedProduct);
                _context.SaveChanges();
                TempData["success"] = "Proizvod je uspješno izmijenjen!";
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }

        }

        public IActionResult Delete(int id)
        {
            try
            {
                var productToDelete = _context.Products.Single(p => p.Id == id);
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
                TempData["success"] = "Proizvod je obrisan!";
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Greška");
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
