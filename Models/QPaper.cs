using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class QPaper
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid title")]
        public string Title { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Enter a valid Subject name")]
        public string Subject { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Enter a valid Description")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.pdf|.docx|.txt)$", ErrorMessage = "Only Document or Image files allowed.")]
        public HttpPostedFileBase Image { get; set; }

        public string Path { get; set; }
        public int Status { get; set; }
        public DateTime DateTime { get; set; }
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
    }
}