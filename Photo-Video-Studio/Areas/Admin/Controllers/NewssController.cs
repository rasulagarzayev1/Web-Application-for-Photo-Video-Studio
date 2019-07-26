using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class NewssController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public NewssController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Admin/News
        public ActionResult Index()
        {
            NewsViewModel vm = new NewsViewModel
            {
                News = db.News.ToList(),
                NewsToTags = db.NewsToTags.ToList(),
                NewsTags = db.NewsTags.ToList()
            };
            return View(vm);
        }

        public ActionResult Create()
        {
            ViewBag.NewsTags = new SelectList(db.NewsTags, "ID", "Tag");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude = "Image")] News news, HttpPostedFileBase Image, IEnumerable<int> NewsTags)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        news.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);
                        news.CreatedAt = news.UpdatedAt = DateTime.Now;

                        News addedNews = null;

                        Task addedNewsTask = Task.Run(() =>
                        {
                            addedNews = db.News.Add(news);
                            db.SaveChanges();
                        });

                        await addedNewsTask;
                        if (NewsTags != null) 
                        {
                            foreach (var TagID in NewsTags)
                            {
                                db.NewsToTags.Add(new NewsToTag
                                {
                                    NewsID = addedNews.ID,
                                    TagID = TagID
                                });
                                db.SaveChanges();
                            }
                        }
                        
                        db.SaveChanges();

                        return RedirectToAction("Index", "Newss");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please choose an image");
                }

            }
            ViewBag.NewsTags = new SelectList(db.NewsTags, "ID", "Tag");
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("ID was not found");

            SingleNewsViewModel singleNewsVM = new SingleNewsViewModel
            {
                News = news,
                NewsToTags = db.NewsToTags.Where(n => n.NewsID == news.ID).ToList()
            };
            return View(singleNewsVM);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("ID was not found");

            return View(news);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            News news = db.News.Find(id);

            List<NewsToTag> newsToTag = db.NewsToTags.Where(n => n.NewsID == id).ToList();
            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images"), news.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            NewsToTag deletednewsToTag = null;

            Task deletedNewsTagTask = Task.Run(() =>
            {
                foreach (var item in newsToTag)
                {
                    deletednewsToTag = db.NewsToTags.Remove(item);
                }
                db.SaveChanges();
            });

            await deletedNewsTagTask;

            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index", "Newss");
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("ID was not found");
            return View(news);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")] News news, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        news.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);
                        news.CreatedAt = news.UpdatedAt = DateTime.Now;

                        db.Entry(news).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Newss");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please choose an image");
                }

            }
            return View();
        }

    }
}