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
    public class ServiceHeaderController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public ServiceHeaderController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/ServiceHeader
        public ActionResult Index()
        {
            return View(db.ServiceHeaders.First());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            ServiceHeader serviceHeader = db.ServiceHeaders.Find(id);

            if (serviceHeader == null)
                return HttpNotFound("ID was not found");

            return View(serviceHeader);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] ServiceHeader serviceHeader, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        serviceHeader.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(serviceHeader).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "ServiceHeader");
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