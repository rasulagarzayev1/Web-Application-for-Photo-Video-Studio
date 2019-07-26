using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(ClientSliderMetadata))]
    public partial class ClientSlider
    {
        private  class ClientSliderMetadata
        {
            [Required(ErrorMessage ="Title must be filled")]
            public string Title { get; set; }
            [Required(ErrorMessage = "Subtitle must be filled")]
            public string Subtitle { get; set; }
        }
    }
}