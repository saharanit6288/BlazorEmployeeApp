using BlazorEmployeeManagementApp2.Shared.Models;

namespace BlazorEmployeeManagementApp2.Client.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<HttpResponseMessage> CreateEmployee(Employee employee);
        Task<HttpResponseMessage> UpdateEmployee(Employee employee);
        Task<HttpResponseMessage> DeleteEmployee(int id);
        Task<HttpResponseMessage> UploadEmployeeImage(MultipartFormDataContent content);
        Task<HttpResponseMessage> DeleteEmployeeImage(string fileName);
    }
}
