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
    public class GalleryController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public GalleryController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Gallery
        public ActionResult Index()
        {
            return View(db.Galleries);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] Gallery gallery, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        gallery.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);

                        db.Galleries.Add(gallery);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Gallery");
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

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Gallery gallery = db.Galleries.Find(id);

            if (gallery == null)
                return HttpNotFound("ID was not found");

            return View(gallery);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Gallery gallery = db.Galleries.Find(id);

            if (gallery == null)
                return HttpNotFound("ID was not found");

            return View(gallery);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Gallery gallery = db.Galleries.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images"), gallery.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.Galleries.Remove(gallery);
            db.SaveChanges();

            return RedirectToAction("Index", "Gallery");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Gallery gallery = db.Galleries.Find(id);

            if (gallery == null)
                return HttpNotFound("ID was not found");

            return View(gallery);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] Gallery gallery, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        gallery.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(gallery).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Gallery");
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