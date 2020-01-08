using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    public class FriendConnection
    {
        [Key]
        public int FriendConnectionId { get; set; }

        
        public string UserId1 { get; set; }

        
        public string UserId2 { get; set; }

        [ForeignKey("UserId1")]
        public virtual UserProfile User1 { get; set; }

        [ForeignKey("UserId2")]
        public virtual UserProfile User2 { get; set; }

        public string Status { get; set; }

        public DateTime ConnectedDate { get; set; }
    }
}