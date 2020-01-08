using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    [NotMapped]
    public class UserViewModel
    {
        public bool IsConnected { get; internal set; }        
        public IQueryable<UserConnections> Urls { get; internal set; }
        public UserProfile Friend { get; internal set; }
        public string Pic { get; internal set; }
        public string ConnectionStatus { get; internal set; }
    }
}