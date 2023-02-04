using BlazorEmployeeManagementApp2.Shared.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmployeeManagementApp2.Shared.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "rsaha.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBrith { get; set; }
        public int DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public string? PhotoPath { get; set; }
        public virtual Department? Department { get; set; }
    }
}
