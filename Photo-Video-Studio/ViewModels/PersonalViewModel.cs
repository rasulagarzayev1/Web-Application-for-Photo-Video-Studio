using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class PersonalViewModel
    {
        public List<Blog> UserBlogs { get; set; }
        public User User { get; set; }
        public BlogHeader BlogHeader { get; set; }
        public List<StaffOrder> StaffOrders { get; set; }
        public List<StudioOrder> StudioOrders { get; set; }
    }
}

