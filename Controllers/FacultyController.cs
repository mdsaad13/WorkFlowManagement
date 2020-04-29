using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowManagement.DAL;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.Controllers
{
    public class FacultyController : Controller
    {
        private Faculty FacultyDetails;

        public FacultyController()
        {
            ViewBag.WhichSideNav = "_FacultyNav";
            ViewBag.WhichDashboard = "Faculty Dashboard";
        }

        private void SetFacultyDetails()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            FacultyDetails = princiUtil.GetFacultyByID(Convert.ToInt32(Session["FacultyID"]));
        }

        public ActionResult Index()
        {
            SetFacultyDetails();
            Common common = new Common();
            MeetingsAndNoticeBundle meetingsAndNoticeBundle = new MeetingsAndNoticeBundle();
            meetingsAndNoticeBundle.NoticeBoard = common.NoticeBoard();
            meetingsAndNoticeBundle.Meetings = common.Meetings(false, FacultyDetails.DeptID);

            return View(meetingsAndNoticeBundle);
        }

        public ActionResult NoticeBoards()
        {
            Common common = new Common();
            return View(common.NoticeBoard(true));
        }

        public ActionResult AddNoticeBoards()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNoticeBoards(NoticeBoard noticeBoard)
        {
            Common common = new Common();
            noticeBoard.Date = DateTime.Now;
            if (common.AddNoticeBoard(noticeBoard))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("NoticeBoards");
        }

        /* Meetings operations starts here */
        public ActionResult Meetings()
        {
            SetFacultyDetails();
            ViewBag.MyDepartmentName = FacultyDetails.DeptName;
            Common common = new Common();
            return View(common.Meetings(true, FacultyDetails.DeptID));
        }

        /* Meetings operations ends here */

        /* Leaves operations starts here */
        [HttpGet]
        public ActionResult Leaves()
        {
            Common common = new Common();

            return View(common.Leaves(2, Convert.ToInt32(Session["FacultyID"])));
        }

        public ActionResult AskLeaves()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AskLeaves(Leaves leaves)
        {
            leaves.AskedBy = 2;
            leaves.FacultyID = Convert.ToInt32(Session["FacultyID"]);
            leaves.Status = 0;
            leaves.HodID = 0;

            Common common = new Common();
            if (common.AddLeave(leaves))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Leaves");
        }

        public ActionResult DeleteLeaves(int ID)
        {
            Common common = new Common();
            common.DeleteLeave(ID);

            Session["Notification"] = 3;

            return RedirectToAction("Leaves");
        }
        /* Leaves operations ends here */
    }
}