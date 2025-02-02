using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class District
    {
        [Key]
        public int? District_ID { get; set; }
        [Display(Name = "District Name")]
        public string District_name { get; set; }
    }
}
