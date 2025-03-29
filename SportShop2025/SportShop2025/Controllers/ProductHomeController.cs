using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportShop2025.Data;
using SportShop2025.ViewModel;
using System.Drawing.Printing;

namespace SportShop2025.Controllers
{
    public class ProductHomeController : Controller
    {
        private readonly SportShop2025Context db;
        public ProductHomeController(SportShop2025Context _db)
        {
            db = _db;
        }
        public IActionResult Index(int page = 1, int pageSize = 4)
        {
            var totalProducts = db.Products.Count();

            var products = db.Products
                             .OrderBy(p => p.ProductId)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var model = new HomeViewModel
            {
                slides = db.Slides?.ToList() ?? new List<Slide>(), // Kiểm tra NULL trước
                videos = db.Videos.Take(2).ToList() ?? new List<Video>(),
                blogs = db.Blogs.Take(4).ToList() ?? new List<Blog>(),
                products = products
            };

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;

            return View(model);
        }



        [Route("/ListProductMan")]
        [HttpGet]
        public IActionResult ListProductMan()
        {
            var list_product_man = db.Products.Where(s=> s.Brand.Contains("Nam")).ToList();
            var model = new HomeViewModel {
                products = list_product_man
            };

            return View(model);
        }


        [Route("/ListProductWomen")]
        [HttpGet]
        public IActionResult ListProductWomen()
        {
            var list_product_woman = db.Products.Where(s => s.Brand.Contains("Nữ")).ToList();
            var model = new HomeViewModel
            {
                products = list_product_woman
            };

            return View(model);
        }

        [Route("/ListProductVideo/{id?}")]
        [HttpGet]
        public IActionResult ListProductVideo(int? id)
        {
            var list_video = db.Videos.ToList();
            var detail_video = db.Videos.FirstOrDefault(s => s.Id == id);
            var list_blog = db.Blogs.ToList();
            var list_product = db.Products.ToList();
            if (detail_video == null && list_video.Count>0) 
            {
                detail_video= list_video.First();
            }
            var model = new HomeViewModel
            {
                video = detail_video,
                blogs = list_blog,
                products=list_product,
                videos = list_video,
            };

            return View(model);
        }


        [Route("/ListProductBlog")]
        [HttpGet]

        public IActionResult ListProductBlog()
        {

            var list_blog = db.Blogs.ToList();
            var list_video = db.Videos.ToList();

            var model = new HomeViewModel
            {
               
                blogs = list_blog,
                videos = list_video,
            };
            return View(model);
        }


        [Route("/DetailProductHome")]
        [HttpGet]
        public IActionResult Details(int? id, int page = 1, int pageSize = 4)
        {
            if (id == null || id == 0)
            {
                return NotFound("Không tìm thấy sản phẩm!");
            }

            var totalProducts = db.Products.Count(s => s.ProductId != id);
            var product = db.Products.Include(s => s.Category)
                                     .FirstOrDefault(s => s.ProductId == id);

            if (product == null)
            {
                return NotFound("Sản phẩm trên không tồn tại trong CSDL!");
            }

            var list_product = db.Products
                                 .Where(s => s.ProductId != id)
                                 .OrderBy(p => p.ProductId)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToList();

            var model = new HomeViewModel
            {
                Product = product,
                products = list_product
            };

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;

            return View(model);
        }

    }
}
