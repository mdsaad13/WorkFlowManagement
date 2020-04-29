using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class Departments
    {
        [Display(Name = "Department ID")]
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid department name")]
        public string Name { get; set; }
    }
}