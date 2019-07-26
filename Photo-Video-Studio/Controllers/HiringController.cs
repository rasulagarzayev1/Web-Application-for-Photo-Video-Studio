using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Controllers
{
    public class HiringController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public HiringController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Hiring
        [AuthorizeUserFilter]
        public ActionResult Index()
        {
            ViewBag.OrderTypes = new SelectList(db.OrderTypes, "ID", "OrderType1");
            return View();
        }

        [HttpPost]
        [AuthorizeUserFilter]
        public ActionResult Index( StaffOrder order)
        {
            if (ModelState.IsValid)
            {
                User user = Session["user"] as User;
                order.UserID = user.ID;
                db.StaffOrders.Add(order);
                
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            db.SaveChanges();
            return View();
        }

       
        [HttpPost]
        [AuthorizeUserFilter]
        public ActionResult Studio(StudioOrder order)
        {
            if (ModelState.IsValid)
            {
                User user = Session["user"] as User;
                order.UserID = user.ID;
                db.StudioOrders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            db.SaveChanges();
            return View();
        }
    }
}
