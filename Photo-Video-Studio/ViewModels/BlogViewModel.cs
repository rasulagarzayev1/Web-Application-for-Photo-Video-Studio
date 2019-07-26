using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<BlogToTag> BlogToTags { get; set; }
        public BlogHeader BlogHeader { get; set; }
        public RecentBlogViewModel RecentBlogViewModel { get; set; }
    }
}



