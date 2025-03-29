using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using SportShop2025.Data;
using System.Net.WebSockets;

namespace SportShop2025.Controllers
{
    public class ManagerCustomerController : Controller
    {
        private readonly SportShop2025Context db;
        public ManagerCustomerController(SportShop2025Context _db)
        {
            db = _db;
        }

        [Route("/ListCustomer")]
        public IActionResult ListCustomer(string? name)
        {
            List<Customer> listcustomer = db.Customers.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                listcustomer = db.Customers.Where(s => s.FullName.ToLower().Contains(name)
                || s.Email.ToLower().Contains(name)
                || s.Address.ToLower().Contains(name)).ToList();
                ViewBag.Data = name;
            }
            else
            {
                ViewBag.Data = null;
            }

            // Kiểm tra nếu không có khách hàng nào sau khi tìm kiếm
            if (!listcustomer.Any())
            {
                ViewBag.Message = "No customers found matching your search criteria.";
            }
            else
            {
                ViewBag.Message = db.Customers.Where(s => s.FullName.Contains(name) || s.Email.Contains(name) || s.Address.Contains(name)).ToList().Count;
            }




            return View(listcustomer);
        }

        [Route("/CreateCustomer")]
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [Route("/CreateCustomer")]
        [HttpPost]

        public IActionResult Create(Customer cs)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(cs);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Add Customer Sucess !";
            }

            return View(cs);
        }



        [Route("/DetailsCustomer")]
        [HttpGet]
        public IActionResult Details(int? id)
        {
            var customer = db.Customers.FirstOrDefault(s => s.CustomerId == id);

            if (customer == null)
            {
                return NotFound("Customer is null !");
            }
            return View(customer);
        }


        [Route("/DeleteCustomer")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = db.Customers.FirstOrDefault(s => s.CustomerId == id);
            if (customer == null)
            {
                return NotFound("No customer !");
            }
            return View(customer);
        }

        [Route("/DeleteCustomer")]
        [HttpPost]
        public IActionResult ConfirmDelete(int CustomerId)
        {
            var customer = db.Customers.Include(s => s.Orders).FirstOrDefault(s => s.CustomerId == CustomerId);
            if (customer == null)
            {
                return NotFound("No customer !");
            }
            if (customer.Orders.Any())
            {
                ViewBag.MessageNoDelete = "This customer currently exists in an order !";
                return View("Delete");
            }
            db.Customers.Remove(customer);
            db.SaveChanges();
            ViewBag.MessageDeleteSuccess = "Delete customer Sucess !";
            return View("Delete");
        }


        [Route("/EditCustomer")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = db.Customers.FirstOrDefault(s => s.CustomerId == id);
            if (customer == null)
            {
                return NotFound("No customer !");
            }
            return View(customer);
        }

        [Route("/EditCustomer")]

        [HttpPost]
        public IActionResult ConfirmEdit(Customer model)
        {
            var customer = db.Customers.Include(s => s.Orders).FirstOrDefault(s => s.CustomerId == model.CustomerId);
            if (customer == null)
            {
                return NotFound("No customer !");
            }
            if (customer.Orders.Any())
            {
                ViewBag.MessageNoDelete = "This customer currently exists in an order !";
                return View("Edit");
            }
            customer.FullName = model.FullName;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            customer.Address = model.Address;
            customer.RegistrationDate = model.RegistrationDate;
            customer.Orders = model.Orders;
            db.SaveChanges();
            ViewBag.SuccessMessage = "Update Customer Sucess !";
            return View("Edit");
        }


    }
}
