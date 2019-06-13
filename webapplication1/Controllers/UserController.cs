using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.DAL;
using System.Net;

namespace WebApplication1.Controllers
{
    
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
           
            using(OurDbContext db=new OurDbContext())
           {
                return View(db.User.ToList());
            }
            
        }
        public ActionResult Details()
        {
            using (OurDbContext db = new OurDbContext())
            {


                if (Session["UserID"] == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                string id = Session["UserID"].ToString();
                User user = db.User.Find(int.Parse(id));
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        public ActionResult LoggedHome()
        {
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
        }

        
        public ActionResult Login()
        {
            return View();
        }

       
        

        [HttpPost]
        public ActionResult Register(User us)
        {
            if (ModelState.IsValid)
            {

                using (OurDbContext db = new OurDbContext())
                {
                    db.User.Add(us);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = us.FirstName +" "+ us.LastName + " successfully registered.";
            }
            return View();

        }
        [HttpPost]
        
        public ActionResult Login(User us)
        {
            using(OurDbContext db=new OurDbContext())
            {
                var usr = db.User.SingleOrDefault(u=>u.Username == us.Username && u.Password == us.Password);
                ViewBag.Users = db.User;
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    if (usr.Username == "Admin")
                    {
                        Session["Admin"] = "logged_in";
                    }
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }
                
            }
            return View();
        }

        
        
        public ActionResult Logout()
        {
            if (Session["UserID"] != null)
            {
                Session["UserID"] = null;
                Session["Username"] = null;
                Session["Admin"] = null;
                Session["cart"] = null;
                return View();
            }
            else RedirectToAction("Index");

            return View();
        }
        
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult LoggedIn(User usr)
        {
           

            return View();
        }


    }
}