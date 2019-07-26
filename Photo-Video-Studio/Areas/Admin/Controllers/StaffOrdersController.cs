using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class StaffOrdersController : Controller
    {
        private PhotoStudioEntities4 db = new PhotoStudioEntities4();

        // GET: Admin/StaffOrders
        public ActionResult Index()
        {
            var staffOrders = db.StaffOrders.Include(s => s.OrderType).Include(s => s.Staff).Include(s => s.User);
            return View(staffOrders.ToList());
        }

        // GET: Admin/StaffOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOrder staffOrder = db.StaffOrders.Find(id);
            if (staffOrder == null)
            {
                return HttpNotFound();
            }
            return View(staffOrder);
        }

        // GET: Admin/StaffOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffOrder staffOrder = db.StaffOrders.Find(id);
            if (staffOrder == null)
            {
                return HttpNotFound();
            }
            return View(staffOrder);
        }

        // POST: Admin/StaffOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffOrder staffOrder = db.StaffOrders.Find(id);
            db.StaffOrders.Remove(staffOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
