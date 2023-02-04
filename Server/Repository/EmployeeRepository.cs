using BlazorEmployeeManagementApp2.Server.Models;
using BlazorEmployeeManagementApp2.Server.Repository.Interface;
using BlazorEmployeeManagementApp2.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorEmployeeManagementApp2.Server.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                var result = await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                var result = _context.Employees.Where(w => w.EmployeeId == employeeId).FirstOrDefault();

                if (result != null)
                {
                    _context.Employees.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await _context.Employees
                .Include(i=>i.Department)
                .Where(w => w.EmployeeId == employeeId).FirstOrDefaultAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.Where(w => w.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchEmployees(string name, Gender? gender)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(w => w.FirstName.Contains(name) || w.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(w => w.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                var result = await _context.Employees.Where(w => w.EmployeeId == employee.EmployeeId).FirstOrDefaultAsync();

                if (result != null)
                {
                    result.FirstName = employee.FirstName;
                    result.LastName = employee.LastName;
                    result.Email = employee.Email;
                    result.DateOfBrith = employee.DateOfBrith;
                    result.Gender = employee.Gender;
                    result.DepartmentId = employee.DepartmentId;
                    result.PhotoPath = employee.PhotoPath;

                    await _context.SaveChangesAsync();

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
