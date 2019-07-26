using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class NewsViewModel
    {
        public List<News> News { get; set; }
        public List<NewsTag> NewsTags { get; set; }
        public List<NewsToTag> NewsToTags { get; set; }
        public NewsHeader NewsHeader { get; set; }
        public List<NewsComment> NewsComments { get; set; }
    }
}





