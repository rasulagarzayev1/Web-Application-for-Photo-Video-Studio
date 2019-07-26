using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class AboutViewModel
    {
        public About About { get; set; }
        public List<ClientSlider> ClientSliders { get; set; }
        public List<Team> Teams { get; set; }
        public List<Partner> Partners { get; set; }
        public AboutHeader AboutHeader { get; set; }

    }
}
