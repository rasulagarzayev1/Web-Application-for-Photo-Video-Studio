﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(StaffMetadata))]
    public partial class Staff
    {
        private class StaffMetadata
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [MinLength(3)]
            public string Firstname { get; set; }
            [Required]
            [MinLength(3)]
            public string Lastname { get; set; }
            [Required]
            [MinLength(6)]
            public string Password { get; set; }
        }
    }
}