using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Models
{
    public class UserEvent
    {
        //[NotMapped]
        //public IList<SelectListItem> CityNames { get; set; }

        //[NotMapped]
        //public IList<SelectListItem> LocationNames { get; set; }

        [Key]
        public int UserEventId { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("Select City")]
        public string CityId { get; set; }

        [Required]
        [DisplayName("Select Location")]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        [DisplayName("Select Starting Date")]
        public DateTime StartingDate { get; set; }

        [Required]
        [DisplayName("Select Ending Date")]
        public DateTime EndingDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User {get;set;}
    }
}