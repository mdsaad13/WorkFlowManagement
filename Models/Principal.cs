using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class Principal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ImgUrl { get; set; }
    }
}