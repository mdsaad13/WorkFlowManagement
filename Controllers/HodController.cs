using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowManagement.DAL;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.Controllers
{
    [HodAuthorize]
    public class HodController : Controller
    {
        // GET: Hod
        public HodController()
        {
            ViewBag.WhichSideNav = "_HodNav";
            ViewBag.WhichDashboard = "HOD Dashboard";
        }

        public ActionResult Index()
        {
            Common common = new Common();
            MeetingsAndNoticeBundle meetingsAndNoticeBundle = new MeetingsAndNoticeBundle();
            meetingsAndNoticeBundle.NoticeBoard = common.NoticeBoard();
            meetingsAndNoticeBundle.Meetings = common.Meetings();

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

    }
}