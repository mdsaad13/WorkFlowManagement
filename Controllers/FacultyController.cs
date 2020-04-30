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
    [FacultyAuthorize]
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

        /* QuestionPapers operations starts here */
        public ActionResult QuestionPapers()
        {
            SetFacultyDetails();
            Common common = new Common();
            return View(common.QPapers(false, FacultyDetails.DeptID, FacultyDetails.ID));
        }
        
        public ActionResult AddQuestionPapers()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuestionPapers(QPaper qPaper)
        {
            SetFacultyDetails();
            qPaper.Status = 0;
            qPaper.FacultyID = FacultyDetails.ID;
            qPaper.DeptID = FacultyDetails.DeptID;
            qPaper.DateTime = DateTime.Now;
            string ImgUrl = string.Empty;
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + qPaper.Image.FileName;
                if (qPaper.Image != null)
                {
                    string path = Server.MapPath("/Images/QPapers/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    qPaper.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = "/Images/QPapers/" + uniqueFileName;
                }
            }
            catch
            { }

            qPaper.Path = ImgUrl;

            Common common = new Common();
            if (common.AddQPapers(qPaper))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("QuestionPapers");
        }

        public ActionResult DeleteQuestionPapers(int ID)
        {
            Common common = new Common();
            common.DeleteQPapers(ID);

            Session["Notification"] = 3;

            return RedirectToAction("QuestionPapers");
        }
        /* QuestionPapers operations ends here */

        public ActionResult Settings()
        {
            SetFacultyDetails();
            return View(FacultyDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Faculty faculty)
        {
            SetFacultyDetails();
            faculty.ID = FacultyDetails.ID;
            faculty.DeptID = FacultyDetails.DeptID;
            faculty.Subject = FacultyDetails.Subject;

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
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Settings");
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection formCollection)
        {
            string OldPassword = Convert.ToString(formCollection["OldPassword"]);
            string NewPassword = Convert.ToString(formCollection["NewPassword"]);
            SetFacultyDetails();
            if (FacultyDetails.Password == OldPassword)
            {
                PrinciUtil princiUtil = new PrinciUtil();
                if (princiUtil.UpdateFacultyPassword(NewPassword, FacultyDetails.ID))
                {
                    Session["Notification"] = 3;
                }
                else
                {
                    Session["Notification"] = 4;
                }
            }
            else
            {
                Session["Notification"] = 5;
            }

            return RedirectToAction("Settings");
        }
    }
}