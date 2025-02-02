using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class P_Type
    {
        [Key]
        public int P_Type_ID { get; set; }
        [Display(Name = "Property Type")]
        public string P_Type_Name { get; set; }
    }
}
