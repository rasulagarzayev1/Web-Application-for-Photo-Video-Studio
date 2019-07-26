﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class AboutHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public AboutHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/AboutHeader
        public ActionResult Index()
        {
            return View(db.AboutHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            AboutHeader aboutHeader = db.AboutHeaders.Find(id);

            if (aboutHeader == null)
                return HttpNotFound("ID was not found");

            return View(aboutHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] AboutHeader aboutHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        aboutHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(aboutHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "AboutHeader");
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