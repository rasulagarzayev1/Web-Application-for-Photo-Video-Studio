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
    public class BlogHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public BlogHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/BlogHeader
        public ActionResult Index()
        {
            return View(db.BlogHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            BlogHeader blogHeader = db.BlogHeaders.Find(id);

            if (blogHeader == null)
                return HttpNotFound("ID was not found");

            return View(blogHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] BlogHeader blogHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        blogHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(blogHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "BlogHeader");
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