using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(ContactMetadata))]
    public partial class Contact
    {
        private class ContactMetadata
        {
            [Required(ErrorMessage = "Location must be filledd")]
            public string Location { get; set; }
            [Required(ErrorMessage = "Phone must be filledd")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Mail must be filledd")]
            public string Mail { get; set; }
            [Required(ErrorMessage = "Skype must be filledd")]
            public string Skype { get; set; }
        }
    }
}