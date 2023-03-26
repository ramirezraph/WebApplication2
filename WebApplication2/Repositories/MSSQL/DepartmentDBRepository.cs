using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Repositories.MSSQL
{
    public class DepartmentDBRepository : IDepartmentDBRepository
    {
        //ctor
        EMSDBContext _EMSDBContext;
        public DepartmentDBRepository(EMSDBContext EMSDBContext)
        {
            _EMSDBContext = EMSDBContext;
        }

        public async Task<Department?> AddDepartment(Department Department)
        {
            await _EMSDBContext.Department.AddAsync(Department);
            await _EMSDBContext.SaveChangesAsync();

            return Department;
        }

        public async Task<Department?> DeleteDepartment(int DeptId)
        {
            var oldDepartment = await GetDepartmentById(DeptId);
            if (oldDepartment != null)
            {
                _EMSDBContext.Department.Remove(oldDepartment);
                _EMSDBContext.SaveChanges();
                return oldDepartment;
            }
            return null;
        }

        public async Task<Department?> GetDepartmentById(int id)
        {
            return await _EMSDBContext.Department.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await _EMSDBContext.Department.ToListAsync();
        }

        public async Task<Department?> UpdateDepartment(int DeptId, Department Department)
        {
            _EMSDBContext.Department.Update(Department);
            await _EMSDBContext.SaveChangesAsync();
            return Department;
        }
    }
}
