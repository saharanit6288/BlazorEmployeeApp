using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmployeeManagementApp2.Shared.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        public string DepartmentName { get; set; }
    }
}
