using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(FactMetadata))]
    public partial class Fact
    {
        private class FactMetadata
        {
            [Required(ErrorMessage = "Experience Count Must be  filled")]
            public Nullable<int> ExperienceCount { get; set; }
            [Required(ErrorMessage = "Project Count Must be  filled")]
            public Nullable<int> ProjectsCount { get; set; }
            [Required(ErrorMessage = "Client Count Must be  filled")]
            public Nullable<int> ClientsCount { get; set; }
            [Required(ErrorMessage = "Award Count Must be  filled")]
            public Nullable<int> AwardsCount { get; set; }
            [Required(ErrorMessage = "Experience Title Must be  filled")]
            public string ExperienceTitle { get; set; }
            [Required(ErrorMessage = "Project Title Must be  filled")]
            public string ProjectsTitle { get; set; }
            [Required(ErrorMessage = "Client Title Must be  filled")]
            public string ClientsTitle { get; set; }
            [Required(ErrorMessage = "Award Title Must be  filled")]
            public string AwardsTitle { get; set; }
            [Required(ErrorMessage = "Experience Subtitle Must be  filled")]
            public string ExperienceSubtitle { get; set; }
            [Required(ErrorMessage = "Project Subtitle Must be  filled")]
            public string ProjectsSubtitle { get; set; }
            [Required(ErrorMessage = "Client Subtitle Must be  filled")]
            public string ClientsSubtitle { get; set; }
            [Required(ErrorMessage = "Award Subtitle Must be  filled")]
            public string AwardsSubtitle { get; set; }
        }
    }
}