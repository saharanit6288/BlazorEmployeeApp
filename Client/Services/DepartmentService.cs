using BlazorEmployeeManagementApp2.Shared.Models;
using System.Net.Http.Json;

namespace BlazorEmployeeManagementApp2.Client.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await _httpClient.GetFromJsonAsync<Department>("api/Department/GetDepartment/" + id);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _httpClient.GetFromJsonAsync<Department[]>("api/Department/GetDepartments");
        }
    }
}
