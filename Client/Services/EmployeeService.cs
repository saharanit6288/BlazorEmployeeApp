using BlazorEmployeeManagementApp2.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorEmployeeManagementApp2.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _httpClient.GetFromJsonAsync<Employee>("api/Employee/GetEmployee/" + id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _httpClient.GetFromJsonAsync<Employee[]>("api/Employee/GetEmployees");
        }

        public async Task<HttpResponseMessage> CreateEmployee(Employee employee)
        {
           return await _httpClient.PostAsJsonAsync("api/Employee/AddEmployee", employee);
        }

        public async Task<HttpResponseMessage> UpdateEmployee(Employee employee)
        {
           return await _httpClient.PutAsJsonAsync("api/Employee/UpdateEmployee", employee);
        }

        public async Task<HttpResponseMessage> DeleteEmployee(int id)
        {
            return await _httpClient.DeleteAsync("api/Employee/DeleteEmployee/" + id);
        }

        public async Task<HttpResponseMessage> UploadEmployeeImage(MultipartFormDataContent content)
        {
            return await _httpClient.PostAsync("api/Employee/UploadEmployeeImage", content);
        }

        public async Task<HttpResponseMessage> DeleteEmployeeImage(string fileName)
        {
            return await _httpClient.PostAsJsonAsync("api/Employee/DeleteEmployeeImage/", fileName);
        }
    }
}
