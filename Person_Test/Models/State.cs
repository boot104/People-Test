using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Test.Models
{
    public class State
    {
        public int StateId { get; set; }

        [Display(Name = "State")]
        public string StateName { get; set; }
    }
}