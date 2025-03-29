using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SportShop2025.Data;

namespace SportShop2025.Controllers
{
    public class ManagerVideoController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerVideoController(SportShop2025Context _db)
        {
            db = _db;
        }

        [Route("/ListVideo")]
        [HttpGet]
        public IActionResult ListVideo()
        {
            List<Video> videoList = db.Videos.ToList();
            return View(videoList);
        }

        [Route("/CreateVideo")]
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [Route("/CreateVideo")]
        [HttpPost]
        public IActionResult Create(Video vid) {
           if(ModelState.IsValid)
            {
                db.Videos.Add(vid);
                db.SaveChanges();
            }
            return View("Create");
        
        }

        [Route("/DetailVideo")]
        [HttpGet]

        public IActionResult Details(int id)
        {
           var video=db.Videos.FirstOrDefault(x => x.Id == id);
           if(video == null)
            {
                return NotFound("Không có video này tồn tại");
            }
            return View(video);
        }

        [Route("/EditVideo")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var video = db.Videos.FirstOrDefault(x => x.Id == id);
            if (video == null)
            {
                return NotFound("Không tìm thấy video");
            }
            return View(video);
        }

        [Route("/EditVideo")]
        [HttpPost]
        public IActionResult Edit(Video vid)
        {
            if (ModelState.IsValid)
            {
                var videoInDb = db.Videos.FirstOrDefault(x => x.Id == vid.Id);
                if (videoInDb == null)
                {
                    return NotFound("Không tìm thấy video");
                }

                videoInDb.Title = vid.Title;
                videoInDb.Url = vid.Url;
                db.SaveChanges();
                return RedirectToAction("ListVideo");
            }
            return View(vid);
        }


        [Route("/DeleteVideo")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var video = db.Videos.FirstOrDefault(x => x.Id == id);
            if (video == null)
            {
                return NotFound("Không tìm thấy video");
            }
            return View(video);
        }


        [Route("/DeleteVideo")]
        [HttpPost]
        public IActionResult ConfirmDeleteVideo(int id)
        {
            var video = db.Videos.FirstOrDefault(x => x.Id == id);
            if (video == null)
            {
                return NotFound("Không tìm thấy video");
            }

            db.Videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("ListVideo");
        }



    }
}
