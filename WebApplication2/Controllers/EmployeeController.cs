using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeDBRepository _repo;
        public EmployeeController(IEmployeeDBRepository repo)
        {
            _repo = repo;
        }

        public IActionResult GetAllEmployees()
        {
            var employeeList = _repo.GetAllEmployees();
            return View(employeeList);
        }

        [HttpGet]
        public IActionResult AddEmployees()
        {
            var deptList = _repo.FetchDepartmentList();
            ViewBag.Departments = deptList;
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployees(Employee newEmployee)
        {
            if (ModelState.IsValid)
            {
                var newEmp = _repo.AddEmployee(newEmployee);
                return RedirectToAction("GetAllEmployees");
            }
            return View(newEmployee);
        }
        [HttpGet]
        public IActionResult EditEmployees(int EmpId)
        {
            var oldEmp = _repo.GetEmployeeById(EmpId);
            return View(oldEmp);
        }
        [HttpPost]
        public IActionResult EditEmployees(Employee newEmployee)
        {
            if (!ModelState.IsValid)
            {

                var newEmp = _repo.UpdateEmployee(newEmployee.Id, newEmployee);
                return RedirectToAction("GetAllEmployees");
            }
            return View();
        }

        public IActionResult GetEmployeeById(int Id)
        {
            var emp = _repo.GetEmployeeById(Id);
            return View(emp);
        }

        public IActionResult DeleteEmployee(int Id)
        {
            _repo.DeleteEmployee(Id);
            return RedirectToAction("GetAllEmployees");
        }
    }
}
