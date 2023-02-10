using BlazorEmployeeManagementApp2.Server.Repository.Interface;
using BlazorEmployeeManagementApp2.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BlazorEmployeeManagementApp2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository,IWebHostEnvironment env)
        {
            _env = env;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try 
            {
                var result = await _employeeRepository.GetEmployees();
                return Ok(result);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEmployee/{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody]Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("No Input");
                }

                var emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                //Upload File
                if (!string.IsNullOrEmpty(employee.FileName))
                {
                    //If we want to create wwwroot folder in Server project
                    //if (string.IsNullOrWhiteSpace(_env.WebRootPath))
                    //{
                    //    _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    //}
                    var path = Path.Combine(_env.ContentRootPath, "images", employee.FileName);
                    var fs = System.IO.File.Create(path);
                    fs.Write(employee.FileContent, 0, employee.FileContent.Length);
                    fs.Close();

                    employee.PhotoPath = Path.Combine("images", employee.FileName);
                }

                var result = await _employeeRepository.AddEmployee(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody]Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("No Input");
                }

                var emp = await _employeeRepository.GetEmployee(employee.EmployeeId);

                if (emp == null)
                {
                    return NotFound("Employee Not Found");
                }

                //Upload File
                if (!string.IsNullOrEmpty(employee.FileName))
                {
                    //Delete File
                    if (!string.IsNullOrEmpty(emp.PhotoPath))
                    {
                        var pathDel = Path.Combine(_env.ContentRootPath, emp.PhotoPath);

                        if (System.IO.File.Exists(pathDel))
                        {
                            System.IO.File.Delete(pathDel);
                        }
                    }

                    var path = Path.Combine(_env.ContentRootPath, "images", employee.FileName);
                    var fs = System.IO.File.Create(path);
                    fs.Write(employee.FileContent, 0, employee.FileContent.Length);
                    fs.Close();

                    employee.PhotoPath = Path.Combine("images", employee.FileName);
                }

                var result = await _employeeRepository.UpdateEmployee(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var emp = await _employeeRepository.GetEmployee(id);

                if (emp == null)
                {
                    return NotFound("Employee Not Found");
                }

                //Delete File
                if (!string.IsNullOrEmpty(emp.PhotoPath))
                {
                    var path = Path.Combine(_env.ContentRootPath, emp.PhotoPath);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                _employeeRepository.DeleteEmployee(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //[Route("UploadEmployeeImage")]
        //public ActionResult<UploadResult> UploadEmployeeImage(IFormFile file)
        //{
        //    var UploadResult = new UploadResult();
        //    string fileName = file.FileName;
        //    var path = Path.Combine(_env.ContentRootPath, "images", fileName);

        //    using FileStream fs = new(path, FileMode.Create);
        //    file.CopyTo(fs);

        //    UploadResult.UploadFilePath = Path.Combine("images", fileName);

        //    return Ok(UploadResult);
        //}

        //[HttpPost]
        //[Route("DeleteEmployeeImage")]
        //public ActionResult<UploadResult> DeleteEmployeeImage([FromBody] string fileName)
        //{
        //    var UploadResult = new UploadResult();

        //    var path = Path.Combine(_env.ContentRootPath, fileName);

        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }

        //    UploadResult.UploadFilePath = "";

        //    return Ok(UploadResult);
        //}



        [HttpGet]
        [Route("SearchEmployees/{name}/{gender?}")]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployees(string name, Gender? gender)
        {
            try
            {
                var result = await _employeeRepository.SearchEmployees(name, gender);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
