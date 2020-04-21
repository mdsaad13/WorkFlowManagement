using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowManagement.Controllers
{
    public class PrincipalAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["PrincipalID"] == null)
            {
                context.Result = new RedirectResult("/");
            }
        }
    }

    public class HodAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["HodID"] == null)
            {
                context.Result = new RedirectResult("/");
            }
        }
    }

    public class FacultyAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["FacultyID"] == null)
            {
                context.Result = new RedirectResult("/");
            }
        }
    }

    public class IsAuthorized : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["PrincipalID"] != null)
            {
                context.Result = new RedirectResult("/Principal");
            }
            else if (context.HttpContext.Session["HodID"] != null)
            {
                context.Result = new RedirectResult("/Hod");
            }
            else if (context.HttpContext.Session["FacultyID"] != null)
            {
                context.Result = new RedirectResult("/Faculty");
            }
        }
    }
}