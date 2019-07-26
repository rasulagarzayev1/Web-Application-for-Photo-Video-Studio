using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class PersonalController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public PersonalController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Personal
        [AuthorizeUserFilter]
        public ActionResult Index()
        {
            User user = Session["user"] as User;
            PersonalViewModel vm = new PersonalViewModel
            {
                User = user,
                StaffOrders = db.StaffOrders.Where(o => o.UserID == user.ID).ToList(),
                StudioOrders = db.StudioOrders.Where(o => o.UserID == user.ID).ToList(),
                UserBlogs = db.Blogs.Where(b => b.UserID == user.ID).ToList()
            };
            return View(vm);
        }
    }
}