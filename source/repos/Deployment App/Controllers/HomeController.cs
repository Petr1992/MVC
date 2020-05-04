using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployment_App.Controllers;
using Deployment_App.Models;
using Oracle.DataAccess.Client;

namespace Deployment_App.Controllers
{
    public class HomeController : Controller
    {
      


        public ActionResult Index()
        {
            if (Globals.Lg != null)               
            {
              ViewBag.UserName = Globals.Lg;


               return  View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        public ActionResult LogOff()
        {

            AccountController.SafeConnect(null,null,null);
            ViewBag.UserName = null;

            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();

           

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);

            // Invalidate the Cache on the Client Side
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Redirect to the Home Page (that should be intercepted and redirected to the Login Page first)
          
           return RedirectToAction("Login", "Account");
        }

      
    }
}