using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class SingleNewsViewModel
    {
        public News News { get; set; }
        public List<NewsTag> NewsTags { get; set; }
        public List<NewsToTag> NewsToTags { get; set; }
        public NewsSingleHeader NewsSingleHeader { get; set; }
        public RecentNewsViewModel RecentNewsVM { get; set; }
        public List<NewsComment> NewsComments { get; set; }
    }
}



