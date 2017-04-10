using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Test.Models
{
    public class Person
    {
        public int PersonId { get; set; }

       
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required")]
        public int CityId { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Required")]
        public int StateId { get; set; }

        //public virtual City City { get; set; }
        //public virtual State State { get; set; }
    }
}