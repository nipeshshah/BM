using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    public class UserConnections
    {
        [Key]
        public int ConnectionId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfile User { get; set; }

        [Required]
        [DisplayName("Application Type")]
        public string AppType { get; set; }

        [Required]
        [DisplayName("User Profile Link")]
        public string AppUrl { get; set; }
    }
}