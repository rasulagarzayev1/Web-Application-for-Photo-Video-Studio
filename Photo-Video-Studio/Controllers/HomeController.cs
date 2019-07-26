using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhotoStudioEntities4 db;

        public HomeController()
        {
            
            db = new PhotoStudioEntities4();
        }
        
        public ActionResult Index()
        {

            HomeViewModel vm = new HomeViewModel
            {
                MainSliders = db.MainSliders.ToList(),
                About = db.Abouts.First(),
                Videos = db.Videos.ToList(),
                ClientSliders = db.ClientSliders.ToList(),
                Galleries = db.Galleries.ToList(),
                Fact = db.Facts.First(),
                Teams = db.Teams.ToList(),
                Partners = db.Partners.ToList()
            };
            return View(vm);
        }

        public ActionResult About()
        {
            AboutViewModel vm = new AboutViewModel
            {
                About = db.Abouts.First(),
                ClientSliders = db.ClientSliders.ToList(),
                Teams = db.Teams.ToList(),
                Partners = db.Partners.ToList(),
                AboutHeader = db.AboutHeaders.First()
            };
            return View(vm);
        }

        public ActionResult Services()
        {
            ServiceViewModel vm = new ServiceViewModel
            {
                Teams = db.Teams.ToList(),
                Fact=db.Facts.First(),
                ServiceHeader = db.ServiceHeaders.First()
            };
            return View(vm);
        }
 
        public ActionResult Contact()
        {
            ContactViewModel vm = new ContactViewModel
            {
                Contact = db.Contacts.First(),
                ContactHeader = db.ContactHeaders.First(),
                Mesagge = db.Mesagges.FirstOrDefault()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Contact(Mesagge mesagge)
        {
            if (ModelState.IsValid)
            {
                db.Mesagges.Add(mesagge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ContactViewModel vm = new ContactViewModel
            {
                Contact = db.Contacts.First(),
                ContactHeader = db.ContactHeaders.First(),
                Mesagge = mesagge
            };
            return View(vm);
        }


        public ActionResult Login()
        {
            return View(db.LoginHeaders.First());
        }

        public ActionResult Register()
        {
            return View(db.RegisterHeaders.First());
        }

    }
}