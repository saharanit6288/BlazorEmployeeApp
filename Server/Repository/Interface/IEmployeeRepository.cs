using BlazorEmployeeManagementApp2.Shared.Models;

namespace BlazorEmployeeManagementApp2.Server.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<IEnumerable<Employee>> SearchEmployees(string name, Gender? gender);
    }
}
