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
    public class PartnerController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public PartnerController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Partner
        public ActionResult Index()
        {
            return View(db.Partners);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")] Partner partner, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        partner.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);

                        db.Partners.Add(partner);
                        db.SaveChanges();

                        return RedirectToAction("Index", "Partner");
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



        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Partner partner = db.Partners.Find(id);

            if (partner == null)
                return HttpNotFound("ID was not found");

            return View(partner);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Partner partner = db.Partners.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images"), partner.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.Partners.Remove(partner);
            db.SaveChanges();

            return RedirectToAction("Index", "Partner");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Partner partner = db.Partners.Find(id);

            if (partner == null)
                return HttpNotFound("ID was not found");

            return View(partner);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] Partner partner, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        partner.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(partner).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Partner");
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