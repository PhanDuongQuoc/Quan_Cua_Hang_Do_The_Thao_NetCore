using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportShop2025.Data;


namespace SportShop2025.Controllers
{
    public class ManagerCategoriesController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerCategoriesController(SportShop2025Context _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult ListCategories(int? categoryId)
        {
            List<Category> categories = db.Categories.ToList();
            if (categoryId.HasValue && categoryId > 0)
            {
                categories = db.Categories.Where(s => s.CategoryId == categoryId.Value).ToList();
                ViewBag.SelectedCategory = categoryId;
            }
            else
            {
                ViewBag.SelectedCategory = null;
            }
            ViewBag.CountCategory = db.Categories.Count();
            ViewBag.Categories = db.Categories.ToList();
            return View(categories);
        }


        [Route("/CreateCategories")]
        [HttpGet]
        public IActionResult CreateCategories()
        {
            return View();
        }
        [Route("/CreateCategories")]
        [HttpPost]
        public IActionResult CreateCategories(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Add Categories Sucess !";
            }
            return View(category);
        }


        [Route("/DeleteCategory")]
        [HttpGet]
        public IActionResult DeleteCategory(int CategoryId)
        {
            var category = db.Categories.FirstOrDefault(s => s.CategoryId == CategoryId);
            if (category == null)
            {
                return NotFound("Category is null !");
            }
            return View(category);
        }

        [Route("/DeleteCategory")]
        [HttpPost]
        public IActionResult ConfirmDeleteCategory(int CategoryId)
        {
            var category = db.Categories.Include(l => l.Products).FirstOrDefault(s => s.CategoryId == CategoryId);
            if (category == null)
            {
                return NotFound("Category is null");
            }
            if (category.Products.Any())
            {
                ViewBag.MessageNoDelete = "This categories currently exists in an product !";
                return View("DeleteCategory");
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            ViewBag.MessageDeleteSuccess = "Delete category sucess !";
            return View("DeleteCategory");
        }

        [Route("/DetailCategory")]
        [HttpGet]
        public IActionResult DetailCategory(int CategoryId)
        {
            var category = db.Categories.FirstOrDefault(s => s.CategoryId == CategoryId);

            if (category == null)
            {
                return NotFound("Category is null !");
            }
            return View(category);
        }

        [Route("/UpdateCategory")]
        [HttpGet]

        public IActionResult UpdateCategory(int CategoryId)
        {
            var category = db.Categories.FirstOrDefault(s => s.CategoryId == CategoryId);
            if (category == null)
            {
                return NotFound("Category is null !");
            }
            return View(category);
        }

        [Route("/UpdateCategory")]
        [HttpPost]

        public IActionResult ConfirmUpdateCategory(Category model)
        {
            var category = db.Categories.Include(s => s.Products).FirstOrDefault(s => s.CategoryId == model.CategoryId);
            if (category == null)
            {
                return NotFound("Category is null !");
            }
            if (category.Products.Any())
            {
                ViewBag.MessageNoDelete = "This categories currently exists in an product !";
                return View("UpdateCategory");
            }
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            db.SaveChanges();
            ViewBag.SuccessMessage = "Update Product Sucess !";
            return View("UpdateCategory");
        }




    }
}
