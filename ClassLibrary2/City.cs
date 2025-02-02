using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class City
    {
        [Key]
        public int? City_id { get; set; } // Make City_id nullable
        [Display(Name = "City Name")]
        public string City_name { get; set; }
    }
}
