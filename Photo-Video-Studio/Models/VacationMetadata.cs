using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photo_Video_Studio.Models
{
    [MetadataType(typeof(VacationMetadata))]
    public partial class Vacation
    {
        private class VacationMetadata
        {
            [Required(ErrorMessage = "Vacation Start date must be choosen")]
            public Nullable<System.DateTime> VacationDateStart { get; set; }
            [Required(ErrorMessage = "Vacation End date must be choosen")]
            public Nullable<System.DateTime> VacationDateEnd { get; set; }
        }
    }
}