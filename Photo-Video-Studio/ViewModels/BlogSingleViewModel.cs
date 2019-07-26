using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class BlogSingleViewModel
    {
        public Blog Blog { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public List<BlogToTag> NewsToTags { get; set; }
        public BlogHeader BlogHeader { get; set; }
        public RecentBlogViewModel RecentBlogVM { get; set; }
        public List<BlogComment> BlogComments { get; set; }
        public Like Like { get; set; }
        public List<Like> Likes { get; set; }
    }
}



