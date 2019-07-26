using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(NewsMetadata))]
    public partial class News
    {
        private class NewsMetadata
        {
            [Required(ErrorMessage ="Title must be filled")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Description must be filled")]
            public string Description { get; set; }
            
        }
    }
}