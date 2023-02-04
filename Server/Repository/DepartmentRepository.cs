using BlazorEmployeeManagementApp2.Server.Models;
using BlazorEmployeeManagementApp2.Server.Repository.Interface;
using BlazorEmployeeManagementApp2.Shared.Models;

namespace BlazorEmployeeManagementApp2.Server.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public Department GetDepartment(int departmentId)
        {
            return _context.Departments.Where(w => w.DepartmentId == departmentId).FirstOrDefault();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
