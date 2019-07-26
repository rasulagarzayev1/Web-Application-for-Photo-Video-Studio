using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class StaffsController : Controller
    {
        private PhotoStudioEntities4 db = new PhotoStudioEntities4();

        [AuthorizeStaffFilter]
        public ActionResult Index()
        {
            Staff staff = Session["staff"] as Staff;
            staffViewModel vm = new staffViewModel
            {
                Staff = staff,
                StaffOrders = db.StaffOrders.Where(o => o.StaffID == staff.ID).ToList(),
            };
            return View(vm);
        }

        [HttpPost]
        [AuthorizeStaffFilter]
        public ActionResult Index(Vacation vacation)
        {
            Staff staff = Session["staff"] as Staff;
            if (ModelState.IsValid)
            {
                vacation.StaffID = staff.ID;
                db.Vacations.Add(vacation);
                db.SaveChanges();
                return View("Index", "Home");
            }
            return View();
        }

        [AuthorizeStaffFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        [HttpPost]
        [AuthorizeStaffFilter]
        public ActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.Password = Crypto.HashPassword(staff.Password);
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                Session.Clear();
                Session["staff"] = staff;
                return RedirectToAction("Index");
            }
            return View(staff);
        }

       
    }
}
