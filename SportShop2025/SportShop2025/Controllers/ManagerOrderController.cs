using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SportShop2025.Data;


namespace SportShop2025.Controllers
{
    public class ManagerOrderController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerOrderController(SportShop2025Context _db)
        {
            db = _db;
        }

        [Route("/ListOrder")]
        [HttpGet]
        public IActionResult ListOrder(int? id)
        {
            List<Order> orders = db.Orders.Include(s => s.Customer).ToList();
            if (id.HasValue && id > 0)
            {
                orders = db.Orders.Where(s => s.OrderId == id).ToList();
                ViewBag.Data = id;
            }
            else
            {
                ViewBag.Data = null;
            }
            return View(orders);
        }

        [Route("/CreateOrder")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "CustomerId", "FullName");
            return View();
        }

        [Route("/CreateOrder")]
        [HttpPost]

        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            return View("Create");
        }


        [Route("/DeleteOrder")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var order = db.Orders.FirstOrDefault(s => s.OrderId == id);
            if (order == null)
            {
                return NotFound("No order !");
            }
            return View(order);
        }

        [Route("/DeleteOrder")]
        [HttpPost]
        public IActionResult CofirmDelete(int OrderId)
        {
            var order = db.Orders.Include(s => s.Payments).FirstOrDefault(s => s.OrderId == OrderId);
            if (order == null)
            {
                return NotFound("No order !");
            }
            if (order.Payments.Any())
            {
                return NotFound("Order exist payment !");
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return View("Delete");

        }

        [Route("/DetailOrder")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = db.Orders.FirstOrDefault(s => s.OrderId == id);
            if (order == null)
            {
                return NotFound("No order !");
            }

            return View(order);
        }

        [Route("/EditOrder")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "CustomerId", "FullName");
            var order = db.Orders.FirstOrDefault(s => s.OrderId == id);
            if (order == null)
            {
                return NotFound("No order !");
            }
            return View(order);
        }

        [Route("/EditOrder")]
        [HttpPost]

        public IActionResult CofirmEdit(Order model)
        {
            ViewBag.CustomerId = new SelectList(db.Customers.ToList(), "CustomerId", "FullName");
            var order = db.Orders.Include(s => s.Payments).FirstOrDefault(s => s.OrderId == model.OrderId);
            if (order == null)
            {
                return NotFound();
            }
            if (order.Payments.Any())
            {
                return NotFound("Order exist payment !");
            }

            order.CustomerId = model.CustomerId;
            order.OrderDate = model.OrderDate;
            order.TotalAmount = model.TotalAmount;
            order.Status = model.Status;
            db.SaveChanges();
            return View("Edit");



        }





    }
}
