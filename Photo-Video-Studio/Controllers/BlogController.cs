using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Photo_Video_Studio.Models;
using Photo_Video_Studio.ViewModels;

namespace Photo_Video_Studio.Controllers
{
    public class BlogController : Controller
    {
        private readonly PhotoStudioEntities4 db;
        public BlogController()
        {
            db = new PhotoStudioEntities4();
        }
        // GET: Blog
        public ActionResult Index()
        {
            BlogViewModel vm = new BlogViewModel
            {
                BlogHeader = db.BlogHeaders.First(),
                Blogs = db.Blogs.OrderByDescending(b => b.UpdatedAt).Take(3).ToList(),
                BlogTags = db.BlogTags.ToList(),
                RecentBlogViewModel = new RecentBlogViewModel
                {
                    Blogs = db.Blogs.OrderByDescending(b => b.UpdatedAt).Take(5).ToList()
                }
                
            };
            return View(vm);
        }

        [AuthorizeUserFilter]
        public ActionResult Create()
        {
            ViewBag.BlogTags = new SelectList(db.BlogTags, "ID", "Tag");
            return View();
        }

        [HttpPost]
        [AuthorizeUserFilter]
        public async Task<ActionResult> Create([Bind(Exclude = "Image")] Blog blog, HttpPostedFileBase Image, IEnumerable<int> BlogTags)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        blog.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);
                        blog.CreatedAt = blog.UpdatedAt = DateTime.Now;
                        User user = Session["user"] as User;
                        blog.UserID = user.ID;
                        Blog addedBlog = null;

                        Task addedBlogTask = Task.Run(() =>
                        {
                            addedBlog = db.Blogs.Add(blog);
                            db.SaveChanges();
                        });

                        await addedBlogTask;
                        if (BlogTags != null)
                        {
                            foreach (var TagID in BlogTags)
                            {
                                db.BlogToTags.Add(new BlogToTag
                                {
                                    BlogID = addedBlog.ID,
                                    TagID = TagID
                                });
                                db.SaveChanges();
                            }
                        }

                        db.SaveChanges();

                        return RedirectToAction("Index", "Blog");
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
            ViewBag.BlogTags = new SelectList(db.BlogTags, "ID", "Tag");
            return View();
        }

        [AuthorizeUserFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Blog blog = db.Blogs.Find(id);

            if (blog == null)
                return HttpNotFound("ID was not found");

            return View(blog);
        }

        [HttpPost]
        [AuthorizeUserFilter]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            Blog blog = db.Blogs.Find(id);

            List<BlogToTag> blogToTags = db.BlogToTags.Where(n => n.BlogID == id).ToList();
            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Public/images"), blog.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            BlogToTag deletedBlogToTag = null;

            Task deletedBlogTagTask = Task.Run(() =>
            {
                foreach (var item in blogToTags)
                {
                    deletedBlogToTag = db.BlogToTags.Remove(item);
                }
                db.SaveChanges();
            });

            await deletedBlogTagTask;

            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }



        [AuthorizeUserFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Blog blog = db.Blogs.Find(id);

            if (blog == null)
                return HttpNotFound("ID was not found");
            return View(blog);
        }

        [HttpPost]
        [AuthorizeUserFilter]
        public ActionResult Edit([Bind(Exclude = "Image")] Blog blog, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        blog.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Public/images"), Image);
                        blog.CreatedAt = blog.UpdatedAt = DateTime.Now;

                        db.Entry(blog).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Blog");
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


        public ActionResult Single(int? id)
        {
            if (id == null)
            {
                return HttpNotFound("ID is missing");
            }

            Blog blog = db.Blogs.Find(id);

            if (blog == null)
            {
                return HttpNotFound("This news does not exists");
            }
            User user = Session["user"] as User;
            BlogSingleViewModel vm = new BlogSingleViewModel
            {
                Blog = blog,
                BlogTags = db.BlogTags.ToList(),
                NewsToTags = db.BlogToTags.ToList(),
                BlogHeader = db.BlogHeaders.First(),
                BlogComments = db.BlogComments.ToList(),
                Likes = db.Likes.Where(l => l.BlogID == blog.ID).ToList(),
                RecentBlogVM = new RecentBlogViewModel
                {
                    Blogs = db.Blogs.Where(n => n.ID != id).OrderByDescending(n => n.UpdatedAt).Take(5).ToList()
                }
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            List<Blog> searchresult = db.Blogs.Where(b => b.Title.Trim().ToUpper().Contains(query.Trim().ToUpper()) ||
            b.Content.Trim().ToUpper().Contains(query.Trim().ToUpper()) || 
            b.User.Firstname.Trim().ToUpper().Contains(query.Trim().ToUpper()) || 
            b.User.Lastname.Trim().ToUpper().Contains(query.Trim().ToUpper())).ToList();

            BlogViewModel vm = new BlogViewModel
            {
                BlogHeader = db.BlogHeaders.First(),
                Blogs = searchresult,
                BlogTags = db.BlogTags.ToList(),
                RecentBlogViewModel = new RecentBlogViewModel
                {
                    Blogs = db.Blogs.OrderByDescending(b => b.UpdatedAt).Take(5).ToList()
                }
            };
            return View(vm);
        }

        public ActionResult Tag(string tag)
        {
            BlogViewModel vm = new BlogViewModel
            {
                BlogHeader = db.BlogHeaders.First(),
                BlogToTags = db.BlogToTags.Where(b => b.BlogTag.Tag.Trim() == tag.Trim()).ToList(),
                BlogTags = db.BlogTags.ToList(),
                RecentBlogViewModel = new RecentBlogViewModel
                {
                    Blogs = db.Blogs.OrderByDescending(b => b.UpdatedAt).Take(5).ToList()
                }
            };
            ViewBag.Tag = tag;
            return View(vm);
        }

        public ActionResult Author(string author)
        {
            BlogViewModel vm = new BlogViewModel
            {
                BlogHeader = db.BlogHeaders.First(),
                BlogToTags = db.BlogToTags.Where(b => b.Blog.User.Firstname.Trim() == author.Trim()).ToList(),
                BlogTags = db.BlogTags.ToList(),
                RecentBlogViewModel = new RecentBlogViewModel
                {
                    Blogs = db.Blogs.OrderByDescending(b => b.UpdatedAt).Take(5).ToList()
                }
            };
            ViewBag.Tag = author;
            return View(vm);
        }


    }
}

