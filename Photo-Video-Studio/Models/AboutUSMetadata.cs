using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(AboutUSMetadata))]
    public partial class About
    {
        private  class AboutUSMetadata
        {
            [Required(ErrorMessage = "First Paragraph must be filledd")]
            public string FirstTextPart { get; set; }
            [Required(ErrorMessage = "Second Paragraph must be filledd")]
            public string SecondTextPart { get; set; }
        }
    }
}