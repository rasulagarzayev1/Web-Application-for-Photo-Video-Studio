using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        [AuthorizeAdminFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}