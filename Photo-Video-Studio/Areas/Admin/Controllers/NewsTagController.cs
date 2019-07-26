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
    public class NewsTagController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public NewsTagController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/NewsTag
        public ActionResult Index()
        {
            return View(db.NewsTags.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(NewsTag newsTag)
        {
            if (ModelState.IsValid)
            {
                db.NewsTags.Add(newsTag);
                db.SaveChanges();

                return RedirectToAction("Index", "NewsTag");
            }

            return View();
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            NewsTag newsTag = db.NewsTags.Find(id);

            if (newsTag == null)
                return HttpNotFound("ID was not found");

            return View(newsTag);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            NewsTag newsTag = db.NewsTags.Find(id);
            db.NewsTags.Remove(newsTag);
            db.SaveChanges();

            return RedirectToAction("Index", "NewsTag");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            NewsTag newsTag = db.NewsTags.Find(id);

            if (newsTag == null)
                return HttpNotFound("ID was not found");

            return View(newsTag);
        }

        [HttpPost]
        public ActionResult Edit(NewsTag newsTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "NewsTag");
            }
            return View();
        }
    }
}