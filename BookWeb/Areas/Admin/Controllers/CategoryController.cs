using Book.DataAccess.Data;
using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
       
        //private readonly ApplicationDbContext dbContext;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Create Method
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("Name", "The Name can not be same as Display Order");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
        //For reaad 
        public IActionResult List()
        {
            List<Category> list = _unitOfWork.Category.GetAll().ToList();
            return View(list);
        }

        //For edit 
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            Category? category = _unitOfWork.Category.Get(u=>u.Id==id);
            //Category? Category1 = _categoryRepo.Categories.FirstOrDefault(u => u.Id== id);
            //Category? category2 = _categoryRepo.Categories.Where(u => u.Id ==id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        //For post Update
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
        //For delete
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
          Category? category = _unitOfWork.Category.Get(u => u.Id == id);
            if (category != null)
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
    }

//Chaging the Mvc to N tier architecture 
}
// must register the service in program.cs dependency injection