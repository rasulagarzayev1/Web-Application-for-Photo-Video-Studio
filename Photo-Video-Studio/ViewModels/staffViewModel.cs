using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class staffViewModel
    {
        public Staff Staff { get; set; }
        public List<StaffOrder> StaffOrders { get; set; }
        public Vacation Vacation { get; set; }
    }
}

