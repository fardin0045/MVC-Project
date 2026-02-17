using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //for database 
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Created Success Fully";
                return RedirectToAction("List", "Product");
            }
            return View();

        }
        //For read data
        public IActionResult List()
        {
            List<Product> list = _unitOfWork.Product.GetAll().ToList();
            return View(list);
        }
        //public IActionResult List(Product obj)
        //{
        //    List<Product> products = _unitOfWork.Products.GetAll.ToList();
        //    return View(products);
        //}
        //for edit 

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product? product = _unitOfWork.Product.Get(u=> u.Id==id);
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
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product Edited SuccessFully";

                return RedirectToAction("List", "Product");

            }
            return View();
        }
        //For delete 
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id) 
        {
            Product? product = _unitOfWork.Product.Get(u=> u.Id ==id);
            if(product != null)
            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();

                TempData["success"] = "Product Deleted SuccessFully";
                return RedirectToAction("List", "Product");

            }
            return View();
        }
       }
}
