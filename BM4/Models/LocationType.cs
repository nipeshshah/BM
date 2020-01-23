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
    [NotMapped]
    public class LocationType
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [DisplayName("Search City")]
        public string CitySearchbox { get; set; }

        [DisplayName("Location Sub Type")]
        public string LocationTypes { get; set; }

        //Dropdown for Location-Type
        //		1 School, 
        //		1 Higher School,
        //		2 College, 
        //		2 Post-Graduation
        //		3 Job, 
        //		4 Tuition Classes,
        //		4 Coaching Classes,
        //		4 Computer Classes,

        [DisplayName("School Name")]
        public string SchoolName { get; set; }

        [DisplayName("Education Board")]
        public string EducationBoard { get; set; }

        [DisplayName("Course / Faculty")]
        public string CourseFaculty { get; set; }

        [DisplayName("College")]
        public string College { get; set; }

        [DisplayName("University")]
        public string University { get; set; }

        [DisplayName("Company")]
        public string Company { get; set; }

        [DisplayName("Subject")]
        public string Subject { get; set; }

        [DisplayName("Classes Name")]
        public string Classes { get; set; }

        [DisplayName("Professor Name")]
        public string Professor { get; set; }

        [NotMapped]
        public virtual List<SubLocationType> SubLocationTypes { get; set; }
    }

    [NotMapped]
    public class SubLocationType
    {
        [Key]
        public int SubLocationId { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("Select City")]
        public string CityId { get; set; }

        //[DisplayName("Location Sub Type")]
        //public string LocationTypes { get; set; }

        [ForeignKey("Select Location Type")]
        public int LocationTypeId { get; set; }

        [DisplayName("Select Location Sub Type")]
        public virtual LocationType LocationType { get; set; }

        [DisplayName("Select Location Sub Type")]
        public int LocationSubType { get; set; }

        
        //Dropdown for Location-Type
        //		1 School, 
        //		1 Higher School,
        //		2 College, 
        //		2 Post-Graduation
        //		3 Job, 
        //		4 Tuition Classes,
        //		4 Coaching Classes,
        //		4 Computer Classes,

        [DisplayName("Standards (Comma Separated i.e. 1st, 2nd, 3rd)")]
        public string Standard { get; set; }

        [DisplayName("Semester / Year (Comma Separated)")]
        public string SemesterYear { get; set; }

        [DisplayName("Department (Comma Separated)")]
        public string Department { get; set; }

        [DisplayName("Batches (Comma Separated)")]
        public string Batches { get; set; }
    }
}