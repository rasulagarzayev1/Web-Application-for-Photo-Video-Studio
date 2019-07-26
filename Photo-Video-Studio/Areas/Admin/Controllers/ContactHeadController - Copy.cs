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
    public class ContactHeadController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public ContactHeadController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/ContactHead
        public ActionResult Index()
        {
            return View(db.ContactHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            ContactHeader contactHeader = db.ContactHeaders.Find(id);

            if (contactHeader == null)
                return HttpNotFound("ID was not found");

            return View(contactHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] ContactHeader contactHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        contactHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(contactHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "ContactHead");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please choose an image");
                }

            }
            return View();
        }

    }
}