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
    public class ErrorHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public ErrorHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/ErrorHeader
        public ActionResult Index()
        {
            return View(db.ErrorHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            ErrorHeader errorHeader = db.ErrorHeaders.Find(id);

            if (errorHeader == null)
                return HttpNotFound("ID was not found");

            return View(errorHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] ErrorHeader errorHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        errorHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(errorHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "ErrorHeader");
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