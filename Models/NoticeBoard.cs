using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class NoticeBoard
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Description")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}