using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories.MSSQL
{
    public class EmployeeDBRepository : IEmployeeDBRepository
    {
        private readonly EMSDBContext _EMSdbContext;

        public EmployeeDBRepository(EMSDBContext EMSDBContext)
        {
            _EMSdbContext = EMSDBContext;
        }

        public async Task<Employee?> AddEmployee(Employee Employee)
        {
            await _EMSdbContext.Employees.AddAsync(Employee);
            await _EMSdbContext.SaveChangesAsync();

            return Employee;
        }

        public async Task<Employee?> DeleteEmployee(int EmployeeId)
        {
            var oldEmployee = await GetEmployeeById(EmployeeId);
            if (oldEmployee != null)
            {
                _EMSdbContext.Employees.Remove(oldEmployee);
                await _EMSdbContext.SaveChangesAsync();
                return oldEmployee;
            }
            return null;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _EMSdbContext.Employees.Include(x => x.Department).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int? EmployeeId)
        {
            return await _EMSdbContext.Employees
                .AsNoTracking()
                .Include(e => e.Department)
                .SingleOrDefaultAsync(x => x.Id == EmployeeId);
        }

        public async Task<Employee?> UpdateEmployee(int EmployeeId, Employee Employee)
        {
            _EMSdbContext.Employees.Update(Employee);
            await _EMSdbContext.SaveChangesAsync();
            return Employee;
        }

        public async Task<List<Department>> FetchDepartmentList()
        {
            return await _EMSdbContext.Department.ToListAsync();
        }
    }
}
