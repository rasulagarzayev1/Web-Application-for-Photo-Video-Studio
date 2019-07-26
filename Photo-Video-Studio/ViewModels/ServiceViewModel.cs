using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class ServiceViewModel
    {
        public Fact Fact { get; set; }
        public List<Team> Teams { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
    }
}
