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
    [PrincipalAuthorize]
    public class PrincipalController : Controller
    {
        public PrincipalController()
        {
            ViewBag.WhichSideNav = "_PrinciNav";
            ViewBag.WhichDashboard = "Principal Dashboard";
        }

        // GET: Principal
        public ActionResult Index()
        {
            GeneralUtil generalUtil = new GeneralUtil();
            ViewData["TotalDepartments"] = generalUtil.Count("departments");
            ViewData["TotalHods"] = generalUtil.Count("hod");
            ViewData["TotalFaculties"] = generalUtil.Count("faculty");
            ViewData["TotalMeetings"] = generalUtil.Count("meetings");

            Common common = new Common();
            MeetingsAndNoticeBundle meetingsAndNoticeBundle = new MeetingsAndNoticeBundle();
            meetingsAndNoticeBundle.NoticeBoard = common.NoticeBoard();
            meetingsAndNoticeBundle.Meetings = common.Meetings();

            return View(meetingsAndNoticeBundle);
        }

        /* Departments operations starts here */
        public ActionResult Departments()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            List<Departments> departments = princiUtil.AllDepts();
            return View(departments);
        }
        
        public ActionResult AddDepartments()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartments(Departments departments)
        {
            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.AddDept(departments))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("Departments");
        }
        
        public ActionResult EditDepartments(int ID)
        {
            PrinciUtil princiUtil = new PrinciUtil();
            return View(princiUtil.GetDeptByID(ID));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartments(Departments departments)
        {
            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.UpdateDept(departments))
            {
                Session["Notification"] = 3;
            }
            else
            {
                Session["Notification"] = 4;
            }
            return RedirectToAction("Departments");
        }
        /* Departments operations ends here */

        /* HOD operations starts here */
        public ActionResult HOD()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            return View(princiUtil.AllHods());
        }
        
        public ActionResult AddHOD()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            ViewBag.DepartmentList = new SelectList(princiUtil.AllDepts(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHOD(Hod hod)
        {
            string ImgUrl = "/Images/HOD/default.svg";
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + hod.Image.FileName;

                if (hod.Image != null)
                {
                    string path = Server.MapPath("/Images/HOD/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    hod.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = "/Images/HOD/" + uniqueFileName;
                }
            }
            catch
            {
                ImgUrl = "/Images/HOD/default.svg";
            }
            hod.ImgUrl = ImgUrl;

            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.AddHod(hod))
            {
                Session["Notification"] = 1;
            }
            else
            {
                Session["Notification"] = 2;
            }
            return RedirectToAction("HOD");
        }

        public ActionResult EditHOD(int ID)
        {
            PrinciUtil princiUtil = new PrinciUtil();
            ViewBag.DepartmentList = new SelectList(princiUtil.AllDepts(), "id", "name");
            return View(princiUtil.GetHodByID(ID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHOD(Hod hod)
        {
            string ImgUrl;
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + hod.Image.FileName;
                if (hod.Image != null)
                {
                    string path = Server.MapPath("/Images/HOD/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    hod.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = "/Images/HOD/" + uniqueFileName;
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
            
            hod.ImgUrl = ImgUrl;

            PrinciUtil princiUtil = new PrinciUtil();
            if (princiUtil.UpdateHod(hod))
            {
                Session["Notification"] = 3;
            }
            else
            {
                Session["Notification"] = 4;
            }
            return RedirectToAction("HOD");
        }

        public ActionResult DeleteHOD(int ID)
        {
            PrinciUtil princiUtil = new PrinciUtil();

            princiUtil.DeleteHod(ID);

            Session["Notification"] = 5;

            return RedirectToAction("HOD");
        }
        /* HOD operations ends here */

        /* Faculty operations starts here */
        public ActionResult Faculty()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            return View(princiUtil.AllFaculty());
        }

        public ActionResult AddFaculty()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            ViewBag.DepartmentList = new SelectList(princiUtil.AllDepts(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFaculty(Faculty faculty)
        {
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

        public ActionResult DeleteFaculty(int ID)
        {
            PrinciUtil princiUtil = new PrinciUtil();

            princiUtil.DeleteFaculty(ID);

            Session["Notification"] = 5;

            return RedirectToAction("Faculty");
        }
        /* Faculty operations ends here */

        /* Meetings operations ends here */
        public ActionResult Meetings()
        {
            Common common = new Common();
            return View(common.Meetings(true));
        }
        
        public ActionResult AddMeetings()
        {
            PrinciUtil princiUtil = new PrinciUtil();
            ViewBag.DepartmentList = new SelectList(princiUtil.AllDepts(), "id", "name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMeetings(Meetings meetings)
        {
            Common common = new Common();
            meetings.AddedBy = "Principle";
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
            Common common= new Common();
            common.DeleteMeeting(ID);

            Session["Notification"] = 3;

            return RedirectToAction("Meetings");
        }

        public ActionResult Leaves()
        {
            return View();
        }

        public ActionResult QuestionPapers()
        {
            return View();
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

    }
}