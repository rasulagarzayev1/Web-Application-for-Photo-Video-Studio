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
    public class LoginHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public LoginHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/LoginHeader
        public ActionResult Index()
        {
            return View(db.LoginHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            LoginHeader loginHeader = db.LoginHeaders.Find(id);

            if (loginHeader == null)
                return HttpNotFound("ID was not found");

            return View(loginHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] LoginHeader loginHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        loginHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(loginHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "LoginHeader");
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