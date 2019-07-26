using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(NewsTagsMetadata))]
    public partial class NewsTag
    {
        private class NewsTagsMetadata
        {

            [Required(ErrorMessage ="Tag name must be filled")]
            public string Tag { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<NewsToTag> NewsToTags { get; set; }
        }
    }
}