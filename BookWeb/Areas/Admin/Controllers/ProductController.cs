using Book.DataAccess.Data;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //for database 
        private readonly ApplicationDbContext _dbProduct;
        public ProductController(ApplicationDbContext dbProdcut)
        {
            _dbProduct = dbProdcut;
        }

        //for create product
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
        //For read data
        public IActionResult List(Product obj)
        {
            List<Product> products = _dbProduct.Products.ToList();
            return View(products);
        }
        //for edit 

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product? product = _dbProduct.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit (Product obj)
        {
            if (ModelState.IsValid)
            {
                _dbProduct.Update(obj);
                _dbProduct.SaveChanges();

                TempData["success"] = "Product Edited SuccessFully";

                return RedirectToAction("List", "Product");

            }
            return View();
        }
        //For delete 
        [HttpPost]
        public IActionResult Delete(Product obj) 
        {
            Product? product = _dbProduct.Products.Find(obj.Id);
            if(product != null)
            {
                _dbProduct.Products.Remove(product);
                _dbProduct.SaveChanges();

                TempData["success"] = "Product Deleted SuccessFully";
                return RedirectToAction("List", "Product");

            }
            return View();
        }
       }
}
