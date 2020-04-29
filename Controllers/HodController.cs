using System;
using System.Collections.Generic;
using System.IO;
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
        private Hod HodDetails;

        // GET: Hod
        public HodController()
        {
            ViewBag.WhichSideNav = "_HodNav";
            ViewBag.WhichDashboard = "HOD Dashboard";
        }

        private void SetHodDetails()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            HodDetails = princiUtil.GetHodByID(Convert.ToInt32(Session["HodID"]));
        }

        public ActionResult Index()
        {
            SetHodDetails();
            Common common = new Common();
            MeetingsAndNoticeBundle meetingsAndNoticeBundle = new MeetingsAndNoticeBundle();
            meetingsAndNoticeBundle.NoticeBoard = common.NoticeBoard();
            meetingsAndNoticeBundle.Meetings = common.Meetings(false, HodDetails.DeptID);

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

        public ActionResult DeleteNoticeBoard(int ID)
        {
            Common common = new Common();
            common.DeleteNoticeBoard(ID);

            Session["Notification"] = 3;

            return RedirectToAction("NoticeBoards");
        }

        /* Faculty operations starts here */
        public ActionResult Faculty()
        {
            SetHodDetails();
            ViewBag.MyDepartmentName = HodDetails.DeptName;
            PrinciUtil princiUtil = new PrinciUtil();
            return View(princiUtil.AllFaculty(HodDetails.DeptID));
        }

        public ActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFaculty(Faculty faculty)
        {
            SetHodDetails();
            faculty.DeptID = HodDetails.DeptID;
            string ImgUrl = "/Images/Faculty/default.svg";
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + faculty.Image.FileName;
                if (faculty.Image != null)
                {
                    string path = Server.MapPath("/Images/Faculty/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    faculty.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = "/Images/Faculty/" + uniqueFileName;
                }
            }
            catch
            {
                ImgUrl = "/Images/Faculty/default.svg";
            }

            faculty.ImgUrl = ImgUrl;

            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.AddFaculty(faculty))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Faculty");
        }

        public ActionResult EditFaculty(int ID)
        {
            PrinciUtil princiUtil = new PrinciUtil();
            ViewBag.DepartmentList = new SelectList(princiUtil.AllDepts(), "id", "name");
            return View(princiUtil.GetFacultyByID(ID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFaculty(Faculty faculty)
        {
            SetHodDetails();
            faculty.DeptID = HodDetails.DeptID;
            string ImgUrl;
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + faculty.Image.FileName;
                if (faculty.Image != null)
                {
                    string path = Server.MapPath("/Images/Faculty/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    faculty.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = "/Images/Faculty/" + uniqueFileName;
                }
                else
                {
                    ImgUrl = "noupdate";
                }
            }
            catch
            {
                ImgUrl = "noupdate";
            }
            faculty.ImgUrl = ImgUrl;

            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.UpdateFaculty(faculty))
            {
                Session["Notification"] = 3;
            }
            else
            {
                Session["Notification"] = 4;
            }
            return RedirectToAction("Faculty");
        }
        /* Faculty operations ends here */

        /* Meetings operations starts here */
        public ActionResult Meetings()
        {
            SetHodDetails();
            ViewBag.MyDepartmentName = HodDetails.DeptName;
            Common common = new Common();
            return View(common.Meetings(true, HodDetails.DeptID));
        }

        public ActionResult AddMeetings()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMeetings(Meetings meetings)
        {
            Common common = new Common();
            meetings.AddedBy = "HOD";
            SetHodDetails();
            meetings.DeptID = HodDetails.DeptID;
            if (common.AddMeeting(meetings))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Meetings");
        }

        public ActionResult DeleteMeetings(int ID)
        {
            Common common = new Common();
            common.DeleteMeeting(ID);

            Session["Notification"] = 3;

            return RedirectToAction("Meetings");
        }
        /* Meetings operations ends here */

        /* Leaves operations starts here */
        [HttpGet]
        public ActionResult Leaves()
        {
            Common common = new Common();

            return View(common.Leaves(1, Convert.ToInt32(Session["HodID"])));
        }
        
        public ActionResult AskLeaves()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AskLeaves(Leaves leaves)
        {
            leaves.AskedBy = 1;
            leaves.FacultyID = 0;
            leaves.Status = 0;
            leaves.HodID = Convert.ToInt32(Session["HodID"]);

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