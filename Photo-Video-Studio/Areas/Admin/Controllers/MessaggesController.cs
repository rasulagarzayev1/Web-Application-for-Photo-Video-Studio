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
    public class MessaggesController : Controller
    {
        private PhotoStudioEntities4 db = new PhotoStudioEntities4();

        // GET: Admin/Messagges
        public ActionResult Index()
        {
            return View(db.Mesagges.ToList());
        }

        // GET: Admin/Messagges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesagge mesagge = db.Mesagges.Find(id);
            if (mesagge == null)
            {
                return HttpNotFound();
            }
            return View(mesagge);
        }

        // GET: Admin/Messagges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesagge mesagge = db.Mesagges.Find(id);
            if (mesagge == null)
            {
                return HttpNotFound();
            }
            return View(mesagge);
        }

        // POST: Admin/Messagges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mesagge mesagge = db.Mesagges.Find(id);
            db.Mesagges.Remove(mesagge);
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
