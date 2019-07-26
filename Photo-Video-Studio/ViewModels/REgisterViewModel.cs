using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Photo_Video_Studio.Models;

namespace Photo_Video_Studio.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public RegisterHeader RegisterHeader { get; set; }
    }
}
