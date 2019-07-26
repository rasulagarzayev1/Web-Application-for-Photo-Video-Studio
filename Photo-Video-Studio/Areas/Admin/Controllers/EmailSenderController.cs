using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class EmailSenderController : Controller
    {
        private readonly PhotoStudioEntities4 _db;

        public EmailSenderController()
        {
            _db = new PhotoStudioEntities4();
        }

     

        [HttpGet]
        public ActionResult SendEmail()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(MailType mail)
        {
            var fromAddress = new MailAddress("agarzayev.rasul@gmail.com", "For Studio Staff");
            var fromPassword = "rasulasadmin123456";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };


            using (var message = new MailMessage())
            {
                foreach (var recipient in _db.Staffs)
                {
                    message.From = fromAddress;
                    message.To.Add(recipient.Email);
                    message.Subject = mail.Subject;
                    message.Body = mail.Body;

                    smtp.Send(message);
                }
            };

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

    }
}