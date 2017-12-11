using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMovie.Models;

namespace TestMovie.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Login to add or edit movies ";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration obj)

        {
            if (ModelState.IsValid)
            {
                MoviesDBEntities db = new MoviesDBEntities();
                db.Registrations.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }

        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]

        public ActionResult Login (TestMovie.Models.Registration user)
        {
            using (MoviesDBEntities db = new MoviesDBEntities())
            {
                {
                    var userDetails = db.Registrations.Where(X => X.FirstName == user.FirstName && X.Password == user.Password).FirstOrDefault();
                    if (userDetails == null)
                    {
                        user.LoginErrorMessage = "Invalid User Name or Password";
                        return View("Login", user);
                    }
                    else
                    {
                        Session["userID"] = userDetails.UserID;
                        Session["UserName"] = userDetails.UserName;
                        return RedirectToAction("Index", "Movies");
                    }
                }
            }
            
                
        }
        public ActionResult LogOut()
        {
            int userID = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Movies()
        {
            return View();
        }


    }
}