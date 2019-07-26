using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(MessageMetadata))]
    public partial class Mesagge
    {
        private class MessageMetadata
        {
            [Required(ErrorMessage = "Firstname must be filled")]
            public string Firstname { get; set; }
            [Required(ErrorMessage = "Lastname must be filled")]
            public string Lastname { get; set; }
            [Required(ErrorMessage = "Email must be filled")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string Email { get; set; }
            [Required(ErrorMessage = "Content must be filled")]
            public string Content { get; set; }
        }
    }
}