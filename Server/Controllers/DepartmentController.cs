using BlazorEmployeeManagementApp2.Server.Repository;
using BlazorEmployeeManagementApp2.Server.Repository.Interface;
using BlazorEmployeeManagementApp2.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEmployeeManagementApp2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                var result = _departmentRepository.GetDepartments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDepartment/{id:int}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            try
            {
                var result = _departmentRepository.GetDepartment(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
