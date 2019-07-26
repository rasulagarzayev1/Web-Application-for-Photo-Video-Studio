using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public AdminAccountController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/AdminAccount
        public ActionResult Login(string username, string password)
        {
            if (username == null)
            {
                ViewBag.LoginError = "Please fill username.";
                return View();
            }
            if (password == null)
            {
                ViewBag.LoginError = "Please fill password.";
                return View();
            }
            Models.Admin admin = db.Admins.FirstOrDefault(u => u.Username.ToUpper().Trim() == username.ToUpper().Trim());
            if (admin != null &&
                Crypto.VerifyHashedPassword(admin.Password, password))
            {
                Session["admin"] = admin;

                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}