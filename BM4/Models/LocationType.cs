using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BM4.Models
{
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

        [DisplayName("Standards (Comma Separated i.e. 1st, 2nd, 3rd)")]
        public string Standard { get; set; }

        [DisplayName("School Name")]
        public string SchoolName { get; set; }

        [DisplayName("Education Board")]
        public string EducationBoard { get; set; }

        [DisplayName("Semester / Year (Comma Separated)")]
        public string SemesterYear { get; set; }

        [DisplayName("Course / Faculty")]
        public string CourseFaculty { get; set; }

        [DisplayName("College")]
        public string College { get; set; }

        [DisplayName("University")]
        public string University { get; set; }

        [DisplayName("Department (Comma Separated)")]
        public string Department { get; set; }

        [DisplayName("Company")]
        public string Company { get; set; }

        [DisplayName("Batches (Comma Separated)")]
        public string Batches { get; set; }

        [DisplayName("Subject")]
        public string Subject { get; set; }

        [DisplayName("Classes Name")]
        public string Classes { get; set; }

        [DisplayName("Professor Name")]
        public string Professor { get; set; }

    }
}