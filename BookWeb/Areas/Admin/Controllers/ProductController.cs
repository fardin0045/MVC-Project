using Book.DataAccess.Data;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbProduct;
        public ProductController(ApplicationDbContext dbProdcut)
        {
            _dbProduct = dbProdcut;  
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {

                _dbProduct.Add(obj);
                _dbProduct.SaveChanges();
                TempData["success"] = "Created Success Fully";
                return RedirectToAction("List", "Product");
            }
            return View();
           
        }
        public IActionResult List(Product obj)
        {
            List<Product> products = _dbProduct.Products.ToList();
            return View(products);
        }
    }
}
