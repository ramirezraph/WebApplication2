using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentDBRepository _repo;
        public DepartmentController(IDepartmentDBRepository repo)
        {
            _repo = repo;
        }

        public IActionResult GetAllDepartments()
        {
            var departmentList = _repo.GetDepartments();
            return View(departmentList);
        }

        [HttpGet]
        public IActionResult AddDepartments()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDepartments(Department newDepartment)
        {
            if (ModelState.IsValid)
            {
                var newDept = _repo.AddDepartment(newDepartment);
                return RedirectToAction("GetAllDepartments");
            }
            return View(newDepartment);
        }
        [HttpGet]
        public IActionResult EditDepartments(int DeptId)
        {
            var oldDept = _repo.GetDepartmentById(DeptId);
            return View(oldDept);
        }
        [HttpPost]
        public IActionResult EditDepartments(Department newDepartment)
        {
            if (!ModelState.IsValid)
            {
                var newDept = _repo.UpdateDepartment(newDepartment.Id, newDepartment);
                return RedirectToAction("GetAllDepartments");
            }
            return View();
        }

        public IActionResult GetDepartmentById(int Id)
        {
            var dept = _repo.GetDepartmentById(Id);
            return View(dept);
        }

        public IActionResult DeleteDepartment(int Id)
        {
            _repo.DeleteDepartment(Id);
            return RedirectToAction("GetAllDepartments");
        }
    }


}
