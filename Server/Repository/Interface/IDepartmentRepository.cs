using BlazorEmployeeManagementApp2.Shared.Models;

namespace BlazorEmployeeManagementApp2.Server.Repository.Interface
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int departmentId);
    }
}
