using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class NewsCommentsController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public NewsCommentsController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/NewsComments
        public ActionResult Index()
        {
            return View(db.NewsComments.ToList());
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            NewsComment newsComment = db.NewsComments.Find(id);

            if (newsComment == null)
                return HttpNotFound("ID was not found");

            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            NewsComment newsComment = db.NewsComments.Find(id);


            db.NewsComments.Remove(newsComment);
            db.SaveChanges();

            return RedirectToAction("Index", "NewsComments");
        }

    }
}