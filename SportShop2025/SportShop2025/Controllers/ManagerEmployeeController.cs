using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SportShop2025.Data;


namespace SportShop2025.Controllers
{
    public class ManagerEmployeeController : Controller
    {

        private readonly SportShop2025Context db;
        public ManagerEmployeeController(SportShop2025Context _db)
        {
            db = _db;
        }

        [Route("/ListEmployee")]
        [HttpGet]
        public IActionResult ListEmployee(string? name)
        {

            List<Employee> employees = db.Employees.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                employees = db.Employees.Where(s => s.FullName.ToLower().Contains(name)
                || s.Email.ToLower().Contains(name)
                || s.Phone.ToLower().Contains(name)).ToList();
                ViewBag.Data = name;
            }
            else
            {
                ViewBag.Data = null;
            }
            return View(employees);
        }

        [Route("/CreateEmployee")]
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }


        [Route("/CreateEmployee")]
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            return View();
        }
        [Route("/DetailEmployee")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = db.Employees.FirstOrDefault(s => s.EmployeeId == id);
            if (employee == null)
            {
                return NotFound("No employee");
            }

            return View(employee);
        }

        [Route("/DeleteEmployee")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = db.Employees.FirstOrDefault(s => s.EmployeeId == id);
            if (employee == null)
            {
                return NotFound("No employee !");
            }
            return View(employee);
        }

        [Route("/DeleteEmployee")]
        [HttpPost]
        public IActionResult ConfirmDelete(int EmployeeId)
        {
            var employee = db.Employees.FirstOrDefault(s => s.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return NotFound("No employee !");
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return View("Delete");
        }

        [Route("/EditEmployee")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = db.Employees.FirstOrDefault(s => s.EmployeeId == id);
            if (employee == null)
            {
                return NotFound("No Employee");
            }

            return View(employee);
        }

        [Route("/EditEmployee")]
        [HttpPost]
        public IActionResult ConfirmEdit(Employee model)
        {
            var employee = db.Employees.FirstOrDefault(s => s.EmployeeId == model.EmployeeId);
            if (employee == null)
            {
                return NotFound("No employee !");
            }
            employee.FullName = model.FullName;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.HireDate = model.HireDate;
            employee.Position = model.Position;
            employee.Salary = model.Salary;
            db.SaveChanges();
            return View("Edit");
        }



    }
}


