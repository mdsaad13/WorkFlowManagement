﻿using System;
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
        }

        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }
    }
}