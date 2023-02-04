using BlazorEmployeeManagementApp2.Shared.Models;

namespace BlazorEmployeeManagementApp2.Client.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int id);
    }
}
