using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class BlogCommentsController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public BlogCommentsController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/Comments
        public ActionResult Index()
        {
            return View(db.BlogComments.ToList());
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            BlogComment blogComment = db.BlogComments.Find(id);

            if (blogComment == null)
                return HttpNotFound("ID was not found");

            return View(blogComment);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            BlogComment blogComment = db.BlogComments.Find(id);


            db.BlogComments.Remove(blogComment);
            db.SaveChanges();

            return RedirectToAction("Index", "BlogComments");
        }

    }
}