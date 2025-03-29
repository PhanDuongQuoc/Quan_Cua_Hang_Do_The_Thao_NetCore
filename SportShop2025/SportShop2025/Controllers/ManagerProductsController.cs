using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportShop2025.Data;


namespace SportShop2025.Controllers
{
    public class ManagerProductsController : Controller
    {
        private readonly SportShop2025Context db;

        public ManagerProductsController(SportShop2025Context _db)
        {
            db = _db;
        }
        [Route("/ListProduct")]
        [HttpGet]
        public IActionResult ListProduct(int? categoryId, int? id, string? name)
        {

            List<Product> products = db.Products.Include(s => s.Category).ToList();
            if (categoryId.HasValue && categoryId > 0)
            {
                products = db.Products.Where(s => s.CategoryId == categoryId.Value).ToList();
                ViewBag.CountProductInCategory = products.Where(s => s.CategoryId == categoryId.Value).Count();
                ViewBag.SelectedCategory = categoryId;
            }
            else
            {
                ViewBag.SelectedCategory = null;
            }
            if (id.HasValue && id > 0)
            {
                products = db.Products.Where(s => s.ProductId == id.Value).ToList();
                ViewBag.Data = id;
            }
            else
            {
                ViewBag.Data = null;
            }
            if (!string.IsNullOrEmpty(name))
            {
                products = db.Products.Where(s => s.ProductName.ToLower().Contains(name)
                || s.Brand.ToLower().Contains(name)
                || s.Color.ToLower().Contains(name)
                || s.Description.ToLower().Contains(name)
                || s.Size.ToString().ToLower().Contains(name)).ToList();
                ViewBag.Data = name;
            }
            else
            {
                ViewBag.Data = null;
            }
            ViewBag.SumProduct = db.Products.Count();
            ViewBag.Categories = db.Categories.ToList();
            return View(products);
        }

        [Route("/CreateListProduct")]
        [HttpGet]
        public IActionResult CreateListProduct()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }



        [Route("/CreateListProduct")]
        [HttpPost]
        public IActionResult CreateListProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Add Product Sucess !";
            }
            return View(product);
        }


        [Route("/DetailProduct")]
        [HttpGet]
        public IActionResult DetailProduct(int ProductId)
        {
            var product = db.Products.FirstOrDefault(s => s.ProductId == ProductId);
            if (product == null)
            {
                return NotFound("Product is null !");
            }
            return View(product);

        }

        [Route("/DeleteProduct")]
        [HttpGet]
        public IActionResult DeleteProduct(int ProductId)
        {
            var product = db.Products.FirstOrDefault(s => s.ProductId == ProductId);
            if (product == null)
            {
                return NotFound("Product is null !");
            }
            ViewBag.MessageDelete = "Do you want delete product !";
            return View(product);
        }

        [Route("/DeleteProduct")]
        [HttpPost]
        public IActionResult ConfirmDeleteProduct(int ProductId)
        {
            var product = db.Products.Include(l => l.OrderDetails).FirstOrDefault(l => l.ProductId == ProductId);
            if (product == null)
            {
                return NotFound("Product is null !");
            }
            if (product.OrderDetails.Any())
            {
                ViewBag.MessageNoDelete = "This product currently exists in an order detail !";
                return View("DeleteProduct");
            }
            db.Products.Remove(product);
            db.SaveChanges();
            ViewBag.MessageDeleteSuccess = "Delete Product Success !";
            return View("DeleteProduct");
        }


        [Route("/UpdateProduct")]
        [HttpGet]
        public IActionResult UpdateProduct(int ProductId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            var product = db.Products.FirstOrDefault(s => s.ProductId == ProductId);

            if (product == null)
            {
                return NotFound("Product is null !");
            }
            return View(product);
        }
        [Route("/UpdateProduct")]
        [HttpPost]
        public IActionResult ConfirmUpadteProduct(Product model)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            var product = db.Products.Include(l => l.OrderDetails).FirstOrDefault(s => s.ProductId == model.ProductId);
            if (product == null)
            {
                return NotFound("Product is null !");
            }
            if (product.OrderDetails.Any())
            {
                ViewBag.MessageNoDelete = "This product currently exists in an order detail !";
                return View("UpdateProduct");
            }

            product.ProductName = model.ProductName;
            product.CategoryId = model.CategoryId;
            product.Brand = model.Brand;
            product.Size = model.Size;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Color = model.Color;
            product.StockQuantity = model.StockQuantity;
            product.ImageUrl = model.ImageUrl ?? "";
            db.SaveChanges();
            ViewBag.SuccessMessage = "Update Product Sucess !";
            return View("UpdateProduct");
        }


    }
}
