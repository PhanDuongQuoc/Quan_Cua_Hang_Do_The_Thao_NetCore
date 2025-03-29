using Microsoft.AspNetCore.Mvc;
using SportShop2025.Data;

namespace SportShop2025.Controllers
{
    public class ManagerBlogController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerBlogController(SportShop2025Context _db)
        {
            db = _db;
        }

        [Route("/ListBlog")]
        [HttpGet]
        public IActionResult ListBlog()
        {
            List<Blog> listblog = db.Blogs.ToList();
            return View(listblog);
        }


        [Route("/CreateBlog")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("/CreateBlog")]
        [HttpPost]
        public IActionResult Create(Blog bl)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(bl);
                db.SaveChanges();
            }
            return View();
        }

        [Route("/DetailBlog")]
        [HttpGet]

        public IActionResult Details(int id)
        {
           var blog = db.Blogs.FirstOrDefault(s=>s.Id ==id);
            if (blog == null)
            {
                return NotFound("Blog này không tồn tại !");
            }
            return View(blog);
        }

        [Route("/DeleteBlog")]
        [HttpGet]

        public IActionResult Delete(int id)
        {
            var blog = db.Blogs.FirstOrDefault(s => s.Id == id);
            if (blog == null)
            {
                return NotFound("Blog này không tồn tại !");
            }
            return View(blog);
        }

        [Route("/DeleteBlog")]
        [HttpPost]

        public IActionResult ConfirmDeleteBlog(int Id) {

            var blog = db.Blogs.FirstOrDefault(s => s.Id == Id);
            if (blog == null)
            {
                return NotFound("Blog này không tồn tại !");
            }
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return View("DeleteBlog");
        }



        [Route("/EditBlog")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blog = db.Blogs.FirstOrDefault(s => s.Id == id);
            if (blog == null)
            {
                return NotFound("Blog này không tồn tại !");
            }
            return View(blog);
        }


        [Route("/EditBlog")]
        [HttpPost]

        public IActionResult ConfirmEditBlog(Blog model)
        {
            var blog = db.Blogs.FirstOrDefault(s => s.Id == model.Id);
            if (blog == null)
            {
                return NotFound("Blog này không tồn tại !");
            }

            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.CreatedAt = model.CreatedAt;
            db.SaveChanges();
            return View("Edit");

        }





    }
}
