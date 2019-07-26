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
    public class VideoController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public VideoController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Video
        public ActionResult Index()
        {
            return View(db.Videos);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] Video video, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null) 
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        video.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images/"), Image);

                        db.Videos.Add(video);
                        db.SaveChanges();

                        return RedirectToAction("Index","Video");
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

            Video video = db.Videos.Find(id);

            if (video == null)
                return HttpNotFound("ID was not found");

            return View(video);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Video video = db.Videos.Find(id);

            if (video == null)
                return HttpNotFound("ID was not found");

            return View(video);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Video video = db.Videos.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images/"), video.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.Videos.Remove(video);
            db.SaveChanges();

            return RedirectToAction("Index","Video");
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Video video = db.Videos.Find(id);

            if (video == null)
                return HttpNotFound("ID was not found");

            return View(video);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] Video video, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        video.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images/"), Image);


                        db.Entry(video).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index","Video");
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