using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //for database 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        //for create product
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(
                _unitOfWork.Category.GetAll(),
                "Id",
                "Name"
            );

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj, IFormFile? file)
        {
           

            if (ModelState.IsValid)
            {
                //for image path to root wwroot

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) 
                { 
                    string fileName= Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\Images\product\" + fileName;
                }
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("List");
            }

            ViewBag.CategoryList = new SelectList(
                _unitOfWork.Category.GetAll(),
                "Id",
                "Name"
            );

            return View(obj);
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
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryList = new SelectList(
                _unitOfWork.Category.GetAll(),
                "Id",
                "Name",
                product.CategoryId
                );
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Product Edited SuccessFully";

                return RedirectToAction("List", "Product");

            }

            ViewBag.CategoryList = new SelectList(
                _unitOfWork.Category.GetAll(),
                "Id",
                "Name",
                obj.CategoryId
            );
            return View(obj);
        }
        //For delete 
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product != null)
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
