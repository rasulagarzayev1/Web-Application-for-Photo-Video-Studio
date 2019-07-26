using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(teamMetadata))]
    public partial class Team
    {
        private  class teamMetadata
        {
            [Required(ErrorMessage = "Fullname must be filledd")]
            public string FullName { get; set; }
            [Required(ErrorMessage = "Job must be filledd")]
            public string Job { get; set; }
        }
    }
}