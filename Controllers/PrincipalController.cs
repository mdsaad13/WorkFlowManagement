using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowManagement.Controllers
{
    [PrincipalAuthorize]
    public class PrincipalController : Controller
    {
        public PrincipalController()
        {
            ViewBag.WhichSideNav = "_PrinciNav";
        }

        // GET: Principal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Departments()
        {
            return View();
        }

        public ActionResult HOD()
        {
            return View();
        }

        public ActionResult Faculty()
        {
            return View();
        }

        public ActionResult Meetings()
        {
            return View();
        }

        public ActionResult Leaves()
        {
            return View();
        }

        public ActionResult QuestionPapers()
        {
            return View();
        }

    }
}