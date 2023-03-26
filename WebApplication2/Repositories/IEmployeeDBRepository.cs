using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IEmployeeDBRepository
    {
        Task<List<Employee>> GetAllEmployees();
        // Return Type -> Function Name -> Parameters
        Task<Employee?> GetEmployeeById(int? EmployeeId);
        Task<Employee?> AddEmployee(Employee Employee);
        Task<Employee?> UpdateEmployee(int EmployeeId, Employee Employee);
        Task<Employee?> DeleteEmployee(int EmployeeId);
        Task<List<Department>> FetchDepartmentList();
    }
}
