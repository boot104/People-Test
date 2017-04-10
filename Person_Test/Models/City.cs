using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Test.Models
{
    public class City
    {
        public int CityId { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }

        public int StateId { get; set; }
    }
}