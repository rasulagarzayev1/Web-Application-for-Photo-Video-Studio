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
    
    public partial class Like
    {
        public int ID { get; set; }
        public Nullable<int> BlogID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }
    }
}
