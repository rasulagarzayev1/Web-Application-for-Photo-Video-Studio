using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }
        public ContactHeader ContactHeader { get; set; }
        public Mesagge Mesagge { get; set; }
    }
}

