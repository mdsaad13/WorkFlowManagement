using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class Meetings
    {
        public int ID { get; set; }

        [Required]
        public int DeptID { get; set; }

        public string DeptName { get; set; }

        public string AddedBy { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Title")]
        public string Title { get; set; }

        [Required]
        public DateTime DateOfMeeting { get; set; }

        [Required]
        public string TimeOfMeeting { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Place")]
        public string Place { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Enter a valid Description")]
        public string Description { get; set; }
    }

    public class MeetingsAndNoticeBundle
    {
        public List<Meetings> Meetings { get; set; }
        public List<NoticeBoard> NoticeBoard { get; set; }
    }
}