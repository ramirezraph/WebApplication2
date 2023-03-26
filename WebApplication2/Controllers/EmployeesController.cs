using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    // DUMB
    // SOLID Principle
    // Single-Responsibility Principle
    // Open-closed Principle
    // Liskov Substitution
    // Interface Segregation
    // Dependency Inversion

    // ViewModel
    // Form?
    // I need more than 1 Model in my View

    public class EmployeesController : Controller
    {
        // private readonly EMSDBContext _emsDbContext;
        IEmployeeDBRepository _employeesRepository;
        IDepartmentDBRepository _departmentRepository;

        public EmployeesController(IEmployeeDBRepository employeesRepository, IDepartmentDBRepository departmentRepository)
        {
            _employeesRepository = employeesRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            // var eMSDBContext = _repo.Employees.Include(e => e.Department);
            return View(await _employeesRepository.GetAllEmployees());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeesRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            CreateEmployeeViewModel createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Departments = await _departmentRepository.GetDepartments()
            };

            return View(createEmployeeViewModel);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel createEmployeeViewModel)
        {
            createEmployeeViewModel.Departments = await _departmentRepository.GetDepartments();

            if (ModelState.IsValid)
            {
                Employee newEmployee = new Employee
                {
                    FullName = createEmployeeViewModel.NewEmployee.FullName,
                    EmailAdress = createEmployeeViewModel.NewEmployee.EmailAdress,
                    DOB = createEmployeeViewModel.NewEmployee.DOB,
                    PhoneNumber = createEmployeeViewModel.NewEmployee.PhoneNumber,
                    DeptId = createEmployeeViewModel.NewEmployee.DeptId
                };

                await _employeesRepository.AddEmployee(newEmployee);

                return RedirectToAction(nameof(Index));
            }

            return View(createEmployeeViewModel);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeesRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            CreateEmployeeViewModel updateEmployeeViewModel = new CreateEmployeeViewModel
            {
                NewEmployee = new EmployeeFormModel
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    EmailAdress = employee.EmailAdress,
                    DOB = employee.DOB,
                    PhoneNumber = employee.PhoneNumber,
                    DeptId = employee.DeptId
                },
                Departments = await _departmentRepository.GetDepartments()
            };

            return View(updateEmployeeViewModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEmployeeViewModel updateEmployeeViewModel)
        {
            var updatedEmployee = await _employeesRepository.GetEmployeeById(updateEmployeeViewModel.NewEmployee.Id);

            if (updatedEmployee is null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeesRepository.UpdateEmployee(updatedEmployee.Id, updatedEmployee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(updatedEmployee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            updateEmployeeViewModel.Departments = await _departmentRepository.GetDepartments();

            return View(updateEmployeeViewModel);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeesRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_employeesRepository.GetAllEmployees() == null)
            {
                return Problem("Entity set 'EMSDBContext.Employees'  is null.");
            }

            var employee = await _employeesRepository.GetEmployeeById(id);

            if (employee is null)
            {
                return NotFound();
            }

            await _employeesRepository.DeleteEmployee(employee.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _employeesRepository.GetEmployeeById(id) is not null;
        }
    }
}
