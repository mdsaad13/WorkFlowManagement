using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowManagement.Controllers
{
    public class FacultyController : Controller
    {
        public FacultyController()
        {
            ViewBag.WhichSideNav = "_FacultyNav";
            ViewBag.WhichDashboard = "Faculty Dashboard";
        }

        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }
    }
}