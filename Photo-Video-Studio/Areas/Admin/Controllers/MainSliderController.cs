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
    public class MainSliderController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public MainSliderController()
        {
            db = new PhotoStudioEntities4();
        }


        // GET: Admin/MainSlider


        public ActionResult Index()
        {
            return View(db.MainSliders);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] MainSlider mainSlider, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null) 
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        mainSlider.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images/mainslider"), Image);

                        db.MainSliders.Add(mainSlider);
                        db.SaveChanges();

                        return RedirectToAction("Index","MainSlider");
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

            MainSlider mainSlider = db.MainSliders.Find(id);

            if (mainSlider == null)
                return HttpNotFound("ID was not found");

            return View(mainSlider);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            MainSlider mainSlider = db.MainSliders.Find(id);

            if (mainSlider == null)
                return HttpNotFound("ID was not found");

            return View(mainSlider);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            MainSlider mainSlider = db.MainSliders.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images/mainslider"), mainSlider.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.MainSliders.Remove(mainSlider);
            db.SaveChanges();

            return RedirectToAction("Index","MainSlider");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            MainSlider mainSlider = db.MainSliders.Find(id);

            if (mainSlider == null)
                return HttpNotFound("ID was not found");

            return View(mainSlider);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] MainSlider mainSlider, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        mainSlider.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images/mainslider"), Image);


                        db.Entry(mainSlider).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index","MainSlider");
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