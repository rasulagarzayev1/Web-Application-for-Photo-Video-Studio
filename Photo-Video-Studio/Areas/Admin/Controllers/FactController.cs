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
    public class FactController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public FactController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Fact
        public ActionResult Index()
        {
            return View(db.Facts.First());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Fact fact = db.Facts.Find(id);

            if (fact == null)
                return HttpNotFound("ID was not found");

            return View(fact);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Fact fact = db.Facts.Find(id);

            if (fact == null)
                return HttpNotFound("ID was not found");

            return View(fact);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] Fact fact, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        fact.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(fact).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Fact");
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