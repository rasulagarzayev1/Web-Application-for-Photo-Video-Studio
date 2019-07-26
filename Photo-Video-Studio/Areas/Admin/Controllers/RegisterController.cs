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
    public class RegisterController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public RegisterController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Register
        public ActionResult Index()
        {
            return View(db.RegisterHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            RegisterHeader registerHeader = db.RegisterHeaders.Find(id);

            if (registerHeader == null)
                return HttpNotFound("ID was not found");

            return View(registerHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] RegisterHeader registerHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        registerHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(registerHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "RegisterHeader");
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