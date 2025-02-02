using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class Government
    {
        [Key]
        public int Government_id { get; set; }
        [Display(Name = "Government Name")]
        public string Government_name { get; set; }
    }
}
