using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SportShop2025.Data;

namespace SportShop2025.Controllers
{
    public class ManagerPaymentController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerPaymentController(SportShop2025Context _db)
        {
            db = _db;
        }
        [Route("/ListPayment")]
        public IActionResult ListPayment()
        {
            List<Payment> payments = db.Payments.Include(s => s.Order).ToList();
            return View(payments);
        }

        [Route("/CreatePayment")]

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            return View();
        }

        [Route("/CreatePayment")]

        [HttpPost]

        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
            }
            return View("Create");
        }

        [Route("/DetailPayment")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var payment = db.Payments.FirstOrDefault(s => s.PaymentId == id);
            if (payment == null)
            {
                return NotFound("No payment !");
            }
            return View(payment);
        }


        [Route("/DeletePayment")]
        [HttpGet]


        public IActionResult Delete(int id)
        {
            var payment = db.Payments.FirstOrDefault(s => s.PaymentId == id);
            if (payment == null)
            {
                return NotFound("No payment !");
            }
            return View(payment);
        }


        [Route("/DeletePayment")]
        [HttpPost]

        public IActionResult ConfirmDelete(int PaymentId)
        {
            var payment = db.Payments.FirstOrDefault(s => s.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound("No payment !");
            }
            db.Payments.Remove(payment);
            db.SaveChanges();
            return View("Delete");
        }


        [Route("/EditPayment")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            var payment = db.Payments.FirstOrDefault(s => s.PaymentId == id);
            if (payment == null)
            {
                return NotFound("No payment !");
            }
            return View(payment);
        }


        [Route("/EditPayment")]
        [HttpPost]

        public IActionResult ConfirmEdit(Payment model)
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            var payment = db.Payments.Include(s => s.Order).FirstOrDefault(s => s.PaymentId == model.PaymentId);
            if (payment == null)
            {
                return NotFound("No payment !");
            }
            payment.OrderId = model.OrderId;
            payment.PaymentMethod = model.PaymentMethod;
            payment.PaymentDate = model.PaymentDate;
            db.SaveChanges();
            return View("Edit");
        }

    }
}
