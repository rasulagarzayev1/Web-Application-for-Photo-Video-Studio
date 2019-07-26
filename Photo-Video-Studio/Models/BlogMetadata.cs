using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(BlogMetadata))]
    public partial class Blog
    {
        private class BlogMetadata
        {
            [Required(ErrorMessage = "Title must be filled")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Content must be filled")]
            public string Content { get; set; }

        }
    }
}