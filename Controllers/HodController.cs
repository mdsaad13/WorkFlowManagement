using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowManagement.Controllers
{
    [HodAuthorize]
    public class HodController : Controller
    {
        // GET: Hod
        public HodController()
        {
            ViewBag.WhichSideNav = "_HodNav";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}