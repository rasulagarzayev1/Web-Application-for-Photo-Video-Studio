using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly PhotoStudioEntities4 db;
        public AccountController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View(db.LoginHeaders.First());
        }

        [HttpPost]
        public ActionResult Login(string email, string password,bool? asStaff)
        {
            if (email == null) 
            {
                ViewBag.LoginError = "Please fill email.";
                return View();
            }
            if (password == null)
            {
                ViewBag.LoginError = "Please fill password.";
                return View();
            }
            if (asStaff == null)
            {
                User user = db.Users.FirstOrDefault(u => u.Email.ToUpper().Trim() == email.ToUpper().Trim());
                if (user != null &&
                    Crypto.VerifyHashedPassword(user.Password, password))
                {
                    Session["user"] = user;

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                Staff staff = db.Staffs.FirstOrDefault(u => u.Email.ToUpper().Trim() == email.ToUpper().Trim());
                if (staff != null &&
                    Crypto.VerifyHashedPassword(staff.Password, password))
                {
                    Session["staff"] = staff;

                    return RedirectToAction("Index", "Home");
                }
            }


            ViewBag.LoginError = "Username or password is wrong.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                if (db.Users.Where(u => u.Email.ToUpper().Trim() == user.Email.ToUpper().Trim()).FirstOrDefault() == null) 
                {
                    db.Users.Add(new User
                    {
                        Email = user.Email,
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Password = Crypto.HashPassword(user.Password)
                    });
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "This email is already exists");
                }
                
            }

            return View();
        }

        [AuthorizeUserFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            User user = db.Users.Find(id);

            if (user == null)
                return HttpNotFound("ID was not found");
            return View(user);

        }

        [HttpPost]
        [AuthorizeUserFilter]
        public ActionResult Edit(User user)
        {

            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                db.Entry(user).State = EntityState.Modified;
                Session.Clear();
                Session["user"] = user;
                db.SaveChanges();
                return RedirectToAction("Index", "Personal");
            }

            return View(user);
        }
    }
}