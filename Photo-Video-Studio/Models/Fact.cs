//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Photo_Video_Studio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fact
    {
        public int ID { get; set; }
        public Nullable<int> ExperienceCount { get; set; }
        public Nullable<int> ProjectsCount { get; set; }
        public Nullable<int> ClientsCount { get; set; }
        public Nullable<int> AwardsCount { get; set; }
        public string ExperienceTitle { get; set; }
        public string ProjectsTitle { get; set; }
        public string ClientsTitle { get; set; }
        public string AwardsTitle { get; set; }
        public string ExperienceSubtitle { get; set; }
        public string ProjectsSubtitle { get; set; }
        public string ClientsSubtitle { get; set; }
        public string AwardsSubtitle { get; set; }
        public string Image { get; set; }
    }
}