using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Panel()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
            { return RedirectToAction("../Home/Home"); }
        }
        public ActionResult UserList()
        {
            if (Session["Admin"] != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    return View(db.User.ToList());
                }
            }
            else
                return View("../Home/Index");
        }

        public ActionResult ItemList()
        {
            if (Session["Admin"] != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    return View(db.Items.ToList());
                }
            }
            else
                return View("../Home/Index");
        }

        public ActionResult AddNewItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewItem(Item newitem)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.Items.Add(newitem);
                    db.SaveChanges();

                    return RedirectToAction("../Admin/ItemList");
                }
            }
            else
            {
                return View("AddNewItem", newitem);
            }
        }

        public ActionResult EditItem(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                Item item = db.Items.Where(i => i.ItemID == id).FirstOrDefault();
                if (Session["Admin"] == null)
                {
                    return RedirectToAction("../Admin/ItemList");
                }
                return View(item);
            }
        }

        [HttpPost]
        public ActionResult EditItem(Item itemtoedit)
        {
            using (OurDbContext db = new OurDbContext())
            {
                db.Entry(itemtoedit).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../Admin/ItemList");
            }
        }

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var itemtodel = db.Items.Where(i => i.ItemID == id).FirstOrDefault();
                db.Items.Remove(itemtodel);
                db.SaveChanges();
                return RedirectToAction("../Admin/ItemList");
            }
        }
    }
}