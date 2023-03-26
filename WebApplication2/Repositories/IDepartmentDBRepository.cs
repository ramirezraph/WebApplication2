using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public interface IDepartmentDBRepository
    {
        Task<List<Department>> GetDepartments();
        Task<Department?> GetDepartmentById(int id);
        Task<Department?> AddDepartment(Department Department);
        Task<Department?> UpdateDepartment(int DeptId, Department Department);
        Task<Department?> DeleteDepartment(int DeptId);
    }
}
