using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Atyati.Models
{
    public partial class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int? Salary { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTime? StartDate { get; set; }
        public string DepartmentName { get; set; }
        [NotMapped]
        public List<SelectListItem> DepartmentNames { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "IT", Text = "IT" },
        new SelectListItem { Value = "HR", Text = "HR" },
        new SelectListItem { Value = "PMO", Text = "PMO"  },
    };
    }
}
