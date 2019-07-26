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
    public class TeamController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public TeamController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Team
        public ActionResult Index()
        {
            return View(db.Teams);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Team team = db.Teams.Find(id);

            if (team == null)
                return HttpNotFound("ID was not found");

            return View(team);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Team team = db.Teams.Find(id);

            if (team == null)
                return HttpNotFound("ID was not found");

            return View(team);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] Team team, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        team.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);


                        db.Entry(team).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Team");
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