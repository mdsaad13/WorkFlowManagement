using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkFlowManagement.DAL;
using WorkFlowManagement.Models;

namespace WorkFlowManagement.Controllers
{
    public class HomeController : Controller
    {
        [IsAuthorized]
        public ActionResult Index()
        {
            ViewBag.PrincipalActive = "active";

            //LoginBundle loginBundle = new LoginBundle();

            //Principal PrincipalCreds = new Principal();
            //PrincipalCreds.Email = "admin@mail.com";
            //PrincipalCreds.Password = "Admin123";
            //loginBundle.PrincipalCreds = PrincipalCreds;
            
            //Hod hod = new Hod();
            //hod.Email = "hod@mail.com";
            //hod.Password = "Hod123";
            //loginBundle.HodCreds = hod;

            //Faculty faculty = new Faculty();
            //faculty.Email = "saad.13.personal@gmail.com";
            //faculty.Password = "sAAD123";
            //loginBundle.FacultyCreds = faculty;

            //return View(loginBundle);
            return View();
        }

        [IsAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginBundle Creds)
        {
            string UserName = string.Empty;
            string Password = string.Empty;
            string Table = string.Empty;
            string Redirect = string.Empty;

            if (Creds.WhichLogin == 1)
            {
                // Principal creds
                UserName = Creds.PrincipalCreds.Email;
                Password = Creds.PrincipalCreds.Password;
                Table = "principal";
                Redirect = "Principal";
                ViewBag.PrincipalActive = "active";
                ViewBag.PrincipalError = "active";
            }
            else if (Creds.WhichLogin == 2)
            {
                // Hod creds
                UserName = Creds.HodCreds.Email;
                Password = Creds.HodCreds.Password;
                Table = "hod";
                Redirect = "Hod";
                ViewBag.HODActive = "active";
                ViewBag.HODError = "active";
            }
            else if (Creds.WhichLogin == 3)
            {
                // Faculty creds
                UserName = Creds.FacultyCreds.Email;
                Password = Creds.FacultyCreds.Password;
                Table = "faculty";
                Redirect = "Faculty";
                ViewBag.TeacherActive = "active";
                ViewBag.TeacherError = "active";
            }

            LoginUtil util = new LoginUtil();
            if (util.Login(UserName, Password, Table))
            {
                return RedirectToAction("Index", Redirect);
            }
            else
            {
                return View();
            }
            
        }

        [IsAuthorized]
        public ActionResult Login()
        {
            return RedirectToAction("Index");
        }
        
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}