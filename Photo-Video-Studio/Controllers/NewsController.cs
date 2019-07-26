using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class NewsController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public NewsController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: News
        public ActionResult Index()
        {
            NewsViewModel vm = new NewsViewModel
            {
                News = db.News.OrderByDescending(n => n.UpdatedAt).Take(4).ToList(),
                NewsTags = db.NewsTags.ToList(),
                NewsHeader = db.NewsHeaders.First(),
                NewsToTags = db.NewsToTags.ToList(),
                NewsComments = db.NewsComments.ToList()
            };
            return View(vm);
        }

        public ActionResult Single(int? id)
        {
            if (id == null) 
            {
                return HttpNotFound("ID is missing");
            }

            News news = db.News.Find(id);

            if (news == null) 
            {
                return HttpNotFound("This news does not exists");
            }

            SingleNewsViewModel vm = new SingleNewsViewModel
            {
                News = news,
                NewsTags = db.NewsTags.ToList(),
                NewsToTags = db.NewsToTags.ToList(),
                NewsSingleHeader = db.NewsSingleHeaders.First(),
                NewsComments = db.NewsComments.ToList(),
                RecentNewsVM = new RecentNewsViewModel
                {
                    News=db.News.Where(n => n.ID != id).OrderByDescending(n=>n.UpdatedAt).Take(5).ToList()
                }
            };
            return View(vm);
        }


        [HttpPost]
        public ActionResult Search(string query)
        {
            List<News> searchresult = db.News.Where(n => n.Title.Trim().ToUpper().Contains(query.Trim().ToUpper()) ||
            n.Description.Trim().ToUpper().Contains(query.Trim().ToUpper())).ToList();

            NewsViewModel vm = new NewsViewModel
            {
                NewsHeader = db.NewsHeaders.First(),
                News = searchresult,
                NewsToTags = db.NewsToTags.ToList(),
                NewsTags = db.NewsTags.ToList()
            };
            return View(vm);
        }

        public ActionResult Tag(string tag)
        {
            NewsViewModel vm = new NewsViewModel
            {
                NewsHeader = db.NewsHeaders.First(),
                NewsToTags = db.NewsToTags.Where(b => b.NewsTag.Tag.Trim() == tag.Trim()).ToList(),
                NewsTags = db.NewsTags.ToList(),
            };
            ViewBag.Tag = tag;
            return View(vm);
        }


    }
}