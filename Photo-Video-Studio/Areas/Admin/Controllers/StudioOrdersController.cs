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
    public class StudioOrdersController : Controller
    {
        private PhotoStudioEntities4 db = new PhotoStudioEntities4();

        // GET: Admin/StudioOrders
        public ActionResult Index()
        {
            var studioOrders = db.StudioOrders.Include(s => s.OrderType).Include(s => s.User);
            return View(studioOrders.ToList());
        }

        // GET: Admin/StudioOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudioOrder studioOrder = db.StudioOrders.Find(id);
            if (studioOrder == null)
            {
                return HttpNotFound();
            }
            return View(studioOrder);
        }

       
        // GET: Admin/StudioOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudioOrder studioOrder = db.StudioOrders.Find(id);
            if (studioOrder == null)
            {
                return HttpNotFound();
            }
            return View(studioOrder);
        }

        // POST: Admin/StudioOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudioOrder studioOrder = db.StudioOrders.Find(id);
            db.StudioOrders.Remove(studioOrder);
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
