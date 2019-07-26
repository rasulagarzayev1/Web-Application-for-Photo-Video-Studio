using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.Extensions;
using System.Data.Entity;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class AboutUsController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        // GET: Admin/AboutUs

        public AboutUsController()
        {
            db = new PhotoStudioEntities4();
        }
        public ActionResult Index()
        {
            return View(db.Abouts.First());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            About about = db.Abouts.Find(id);

            if (about == null)
                return HttpNotFound("ID was not found");

            return View(about);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            About about = db.Abouts.Find(id);

            if (about == null)
                return HttpNotFound("ID was not found");

            return View(about);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] About about, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        about.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(about).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index","AboutUS");
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