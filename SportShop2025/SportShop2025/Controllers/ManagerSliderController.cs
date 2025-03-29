using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using SportShop2025.Data;

namespace SportShop2025.Controllers
{
    public class ManagerSliderController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerSliderController(SportShop2025Context _db)
        {
            db = _db;
        }


        [Route("/ListSlide")]
        public IActionResult ListSlide()
        {
            List<Slide> slideList = db.Slides.ToList();
            return View(slideList);
        }

        [Route("/CreateSlide")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("/CreateSlide")]
        [HttpPost]

        public IActionResult Create(Slide slide)
        {
            if(ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();
            }
            return View("Create");
        }


        [Route("/DeleteSlide")]

        [HttpGet]

        public IActionResult DeleteSlide(int id)
        {
            var slide = db.Slides.FirstOrDefault(s=>s.Id == id);
            if (slide == null)
            {
                return NotFound("Sản phẩm bạn muốn xóa hiện không có !");
            }
            return View(slide);
        }


        [Route("/DeleteSlide")]

        [HttpPost]

        public IActionResult ConfirmDeleteSlide(int Id)
        {
            var slide = db.Slides.FirstOrDefault(s => s.Id == Id);
            if (slide == null)
            {
                return NotFound("Sản phẩm bạn muốn xóa hiện không có !");
            }
            db.Slides.Remove(slide);
            db.SaveChanges();
            return View("DeleteSlide");
        }

        [Route("/DetailSlide")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var slide = db.Slides.FirstOrDefault(s=>s.Id==id);
            if(slide == null)
            {
                return NotFound("Không có slide bạn đang kiếm !");
            }

            return View(slide);
        }

        [Route("/EditSlide")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slide = db.Slides.FirstOrDefault(s => s.Id == id);
            if (slide == null)
            {
                return NotFound("Không có slide bạn đang kiếm !");
            }

            return View(slide);
        }

        [Route("/EditSlide")]
        [HttpPost]
        public IActionResult ConfirmEdit(Slide sl)
        {
            var slide = db.Slides.FirstOrDefault(s => s.Id == sl.Id);
            if (slide == null)
            {
                return NotFound("Không có slide bạn đang kiếm !");
            }

            slide.Image = sl.Image;
            slide.Title = sl.Title;
            db.SaveChanges();
            return View("Edit");
        }





    }
}
