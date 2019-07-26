using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(SliderMetadata))]
    public partial class MainSlider
    {
        private class SliderMetadata
        {
            [Required(ErrorMessage = "Header must be filledd")]
            public string RotateHeader { get; set; }
            [Required(ErrorMessage = "Title must be filledd")]
            public string MainTitle { get; set; }
            [Required(ErrorMessage = "Subtitle must be filledd")]
            public string Subtitle { get; set; }
        }

    }
}