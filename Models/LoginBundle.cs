using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkFlowManagement.Models
{
    public class LoginBundle
    {
        public Principal PrincipalCreds { get; set; }
        public Hod HodCreds { get; set; }
        public Faculty FacultyCreds { get; set; }

        public int WhichLogin { get; set; }
    }
}