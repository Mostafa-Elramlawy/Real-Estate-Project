using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class Status
    {
        [Key]
        public int Status_ID { get; set; }
        [Display(Name = "Status Name")]
        public string Status_Name { get; set; }
    }
}
