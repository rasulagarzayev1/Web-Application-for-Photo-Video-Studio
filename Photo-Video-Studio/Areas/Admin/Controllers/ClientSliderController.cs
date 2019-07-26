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
    public class ClientSliderController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public ClientSliderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/ClientSlider
        public ActionResult Index()
        {
            return View(db.ClientSliders);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] ClientSlider clientSlider, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        clientSlider.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);

                        db.ClientSliders.Add(clientSlider);
                        db.SaveChanges();

                        return RedirectToAction("Index", "ClientSlider");
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

            ClientSlider clientSlider = db.ClientSliders.Find(id);

            if (clientSlider == null)
                return HttpNotFound("ID was not found");

            return View(clientSlider);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            ClientSlider clientSlider = db.ClientSliders.Find(id);

            if (clientSlider == null)
                return HttpNotFound("ID was not found");

            return View(clientSlider);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            ClientSlider clientSlider = db.ClientSliders.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images"), clientSlider.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.ClientSliders.Remove(clientSlider);
            db.SaveChanges();

            return RedirectToAction("Index", "ClientSlider");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            ClientSlider clientSlider = db.ClientSliders.Find(id);

            if (clientSlider == null)
                return HttpNotFound("ID was not found");

            return View(clientSlider);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] ClientSlider clientSlider, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        clientSlider.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(clientSlider).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "ClientSlider");
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
