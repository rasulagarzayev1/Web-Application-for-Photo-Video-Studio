using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class ContactController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public ContactController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(db.Contacts.First());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Contact contact = db.Contacts.Find(id);

            if (contact == null)
                return HttpNotFound("ID was not found");

            return View(contact);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Contact contact = db.Contacts.Find(id);

            if (contact == null)
                return HttpNotFound("ID was not found");

            return View(contact);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();

            return RedirectToAction("Index", "Contact");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Contact contact = db.Contacts.Find(id);

            if (contact == null)
                return HttpNotFound("ID was not found");

            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit( Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Contact");
            }
            return View();
        }

    }
}