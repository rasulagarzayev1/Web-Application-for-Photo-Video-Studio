using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class AJAXController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public AJAXController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: AJAX
        [AuthorizeUserFilter]
        public ActionResult CreateComment(string content, int blogID)
        {

            User user = Session["user"] as User;

            BlogComment comment = new BlogComment
            {
                Content = content,
                UserID = user.ID,
                BlogID = blogID,
                CommentDate = DateTime.Now
            };

            db.BlogComments.Add(comment);
            db.SaveChanges();
            comment.User = user;

            return PartialView("_PartialComment", comment);

        }

        [AuthorizeUserFilter]
        public ActionResult CreateNewsComment(string content, int newsID)
        {

            User user = Session["user"] as User;

            NewsComment comment = new NewsComment
            {
                Content = content,
                UserID = user.ID,
                NewsID = newsID,
                Commentdate = DateTime.Now
            };

            db.NewsComments.Add(comment);
            db.SaveChanges();
            comment.User = user;

            return PartialView("_PartialNewsComment", comment);

        }

        [AuthorizeUserFilter]
        public ActionResult LikeBlog(int blogID)
        {
            User user = Session["user"] as User;

            Like like = new Like
            {
                BlogID = blogID,
                UserID = user.ID
            };
            db.Likes.Add(like);
            db.SaveChanges();
            return Json(like.ID);

        }

        [AuthorizeUserFilter]
        public ActionResult DisLikeBlog(int likeID)
        {
            User user = Session["user"] as User;
            Like dislike = db.Likes.Where(l => l.ID == likeID).First();
            db.Likes.Remove(dislike);
            db.SaveChanges();
            return Json(dislike,JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadBlogs(int skip)
        {
            return PartialView("_PartialBlog", db.Blogs.OrderByDescending(b => b.UpdatedAt).Skip(skip).Take(3).ToList());
        }

        public ActionResult LoadNews(int skip)
        {
            NewsViewModel vm = new NewsViewModel
            {
                News = db.News.OrderByDescending(n => n.UpdatedAt).Skip(skip).Take(3).ToList(),
                NewsToTags = db.NewsToTags.ToList()
            };
            return PartialView("_PartialNews",vm);
        }

        public ActionResult LoadPhotographs(string date)
        {
            DateTime Date = Convert.ToDateTime(date);
            List<Staff> staffs = db.Staffs.Where(s => s.AsPhotograph == true).ToList();
            StaffOrder orderedPhotograph = db.StaffOrders.Where(p => p.OrderDate.Value == Date && p.Staff.AsPhotograph==true).FirstOrDefault();
            for (int i = 0; i < staffs.Count; i++)
            {
                if (orderedPhotograph != null && staffs[i] == orderedPhotograph.Staff) 
                {
                    staffs.Remove(staffs[i]);
                }
            }
            return PartialView("_PartialLoadPhotographs", staffs);
        }

        //public ActionResult LoadPhotoSpeciality(int orderTypeID)
        //{
        //    return PartialView("_PartialPhotographsForOrderType", db.Staffs.Where(s=>s.ProfessionID == orderTypeID && s.AsPhotograph==true).ToList());
        //}

        //public ActionResult PartialLoadVideographsForOrderTypes(int orderTypeID)
        //{
        //    return PartialView("_PartialLoadVideographs", db.Staffs.Where(s => s.AsVideograph == true && s.OrderType.ID == orderTypeID).ToList());
        //}

        public ActionResult LoadVideographsforDate(string date)
        {
            DateTime Date = Convert.ToDateTime(date);
            List<Staff> staffs = db.Staffs.Where(s => s.AsVideograph == true).ToList();
            StaffOrder orderedVideoGraph = db.StaffOrders.Where(p => p.OrderDate == Date && p.Staff.AsVideograph == true).FirstOrDefault();
            for (int i = 0; i < staffs.Count; i++)
            {
                if (orderedVideoGraph != null && staffs[i] == orderedVideoGraph.Staff)
                {
                    staffs.Remove(staffs[i]);
                }
            }
            return PartialView("_PartialLoadVideographs", staffs);
        }

        public ActionResult LoadStudioforDate(string date)
        {
            DateTime Date = Convert.ToDateTime(date);
            List<OrderType> orderTypes = db.OrderTypes.ToList();
            StudioOrder OrderedstudioType = db.StudioOrders.Where(p => p.OrderDate == Date).FirstOrDefault();

            for (int i = 0; i < orderTypes.Count; i++)
            {
                if (OrderedstudioType != null && orderTypes[i] == OrderedstudioType.OrderType)
                {
                    orderTypes.Remove(orderTypes[i]);
                }
            }
            return PartialView("_StudioTypesPartial", orderTypes);
        }

        [AuthorizeStaffFilter]
        public ActionResult TakeVacation(string startDate,string endDate)
        {
            DateTime startTime = Convert.ToDateTime(startDate);
            DateTime endTime = Convert.ToDateTime(endDate);

            if (endTime.Year - startTime.Year >= 1 ||
                (startTime.Day <= 15 && endTime.Day > 15 && endTime.Month - startTime.Month >= 1)||
                (startTime.Day>15 && endTime.Day>15 && endTime.Day-startTime.Day!=0 && endTime.Month - startTime.Month >= 1)||
                (startTime.Day < 15 && endTime.Day < 15 && endTime.Day - startTime.Day != 0 && endTime.Month - startTime.Month >= 1) ||
                endTime.Month - startTime.Month>1)
            {
                return PartialView("_PartialVacation");
            }
            return Content("");
        }

        
    }
}