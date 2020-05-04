using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Atyati.Models
{
    public class Employee
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
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
      
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public int Salary { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        public string DepartmentName { get; set; }

        [NotMapped]
        public List<SelectListItem> DepartmentNames { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "IT", Text = "IT" },
        new SelectListItem { Value = "HR", Text = "HR" },
        new SelectListItem { Value = "PMO", Text = "PMO"  },
    };
        //public virtual Department Department { get; set; }
    }
}
