using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BM4.Models
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }

        [DisplayName("Select City")]
        public string Title { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
    [Column(TypeName = "datetime2")]
    public DateTime DateOfBirth { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePic { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Referral Code")]
        public string ReferralCode { get; set; }


        //   (UserId (FB)
        //DisplayTitle
        //FirstName
        //MiddleName
        //LastName
        //Email (FB)
        //Mobile (FB)
        //DateOfBirth
        //ProfilePic
        //City,State,Country,PinCode
        //ReferredBy
        //Joining Date (Current Date readonly)
        //Status (Active)
        //ReferralCode (Auto Generate))

    }
}
