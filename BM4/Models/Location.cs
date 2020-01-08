using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        public virtual MainLocation MainLocation { get; set; }
        
        public string LocationTypes { get; set; }

        public string Text1 { get; set; }
        
    }

    public class MainLocation
    {
        [Key]
        public int MainLocationId { get; set; }

        [Required]
        public string City { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public string Text4 { get; set; }
    }
}