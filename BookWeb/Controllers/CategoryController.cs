using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                dbContext.Categories.Add(obj);
                dbContext.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
        //For reaad 
        public IActionResult List()
        {
            List<Category> list = dbContext.Categories.ToList();
            return View(list);
        }

        //For edit 
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            Category? category = dbContext.Categories.Find(id);
            Category? Category1 = dbContext.Categories.FirstOrDefault(u => u.Id== id);
            Category? category2 = dbContext.Categories.Where(u => u.Id ==id).FirstOrDefault();
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
                dbContext.Update(obj);
                dbContext.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
        //For delete
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
          Category? category = dbContext.Categories.Find(obj.Id);
            if(category != null)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToAction("List", "Category");
            }
            return View();
        }
    }
}
