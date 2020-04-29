using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class Leaves
    {
        public int ID { get; set; }

        /// <summary>
        /// 1 - HOD, 2 - Faculty
        /// </summary>
        public string AskedBy { get; set; }
        public int HodID { get; set; }
        public int FacultyID { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Reason")]
        public string Reason { get; set; }
        /// <summary>
        /// 1 - pending, 2 - accepted
        /// </summary>
        public int Status { get; set; }
    }
}