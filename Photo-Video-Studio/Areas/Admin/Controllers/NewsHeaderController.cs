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
    public class NewsHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public NewsHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/NewsHeader
        public ActionResult Index()
        {
            return View(db.NewsHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            NewsHeader newsHeader = db.NewsHeaders.Find(id);

            if (newsHeader == null)
                return HttpNotFound("ID was not found");

            return View(newsHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] NewsHeader newsHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        newsHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(newsHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "NewsHeader");
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