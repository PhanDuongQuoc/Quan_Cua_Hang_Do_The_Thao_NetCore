using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportShop2025.Data;


namespace SportShop2025.Controllers
{
    public class ManagerOrderDetailController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerOrderDetailController(SportShop2025Context _db)
        {
            db = _db;
        }
        [Route("/ListOrderDetail")]
        public IActionResult ListOrderDetail()
        {
            List<OrderDetail> orderdetails = db.OrderDetails
                .Include(s => s.Order)
                .Include(s => s.Product).ToList();
            return View(orderdetails);
        }

        [Route("/Create_OrderDetail")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            return View();
        }

        [Route("/Create_OrderDetail")]
        [HttpPost]

        public IActionResult Create(OrderDetail orderdetail)
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderdetail);
                db.SaveChanges();
            }
            return View();
        }

        [Route("/Detail_OrderDetail")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var orderdetail = db.OrderDetails.FirstOrDefault(s => s.OrderDetailId == id);
            if (orderdetail == null)
            {
                return NotFound("No order detail !");
            }
            return View(orderdetail);
        }

        [Route("/Delete_OrderDetail")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var orderdetail = db.OrderDetails.FirstOrDefault(s => s.OrderDetailId == id);
            if (orderdetail == null)
            {
                return NotFound("No order detail !");
            }
            return View(orderdetail);
        }


        [Route("/Delete_OrderDetail")]
        [HttpPost]
        public IActionResult CofirmDelete(int OrderDetailId)
        {
            var orderdetail = db.OrderDetails.Include(s => s.Product)
                .Include(s => s.Order)
                .FirstOrDefault(s => s.OrderDetailId == OrderDetailId);
            if (orderdetail == null)
            {
                return NotFound("No order !");
            }
            db.OrderDetails.Remove(orderdetail);
            db.SaveChanges();
            return View("Delete");
        }

        [Route("/Edit_OrderDetail")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            var orderdetail = db.OrderDetails.FirstOrDefault(s => s.OrderDetailId == id);
            if (orderdetail == null)
            {
                return NotFound("No order detail !");
            }

            return View(orderdetail);
        }

        [Route("/Edit_OrderDetail")]
        [HttpPost]
        public IActionResult ConfirmEdit(OrderDetail model)
        {
            ViewBag.OrderId = new SelectList(db.Orders.ToList(), "OrderId", "OrderId");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "ProductId", "ProductName");
            var orderdetail = db.OrderDetails.FirstOrDefault(s => s.OrderDetailId == model.OrderDetailId);
            if (orderdetail == null)
            {
                return NotFound("No order detail !");
            }

            orderdetail.OrderId = model.OrderId;
            orderdetail.ProductId = model.ProductId;
            orderdetail.Quantity = model.Quantity;
            orderdetail.UnitPrice = model.UnitPrice;
            orderdetail.SubTotal = model.SubTotal;
            db.SaveChanges();
            return View("Edit");
        }


    }
}
