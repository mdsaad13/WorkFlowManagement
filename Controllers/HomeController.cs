using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.Controllers
{
    public class HomeController : Controller
    {
        [IsAuthorized]
        public ActionResult Index()
        {
            // This is login page
            return View();
        }

        [IsAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginBundle Creds)
        {
            // This is login page
            return View();
        }
    }
}