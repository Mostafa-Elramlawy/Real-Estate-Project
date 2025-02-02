using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ClassLibrary2
{
    public class CraftsMen2
    {
        [Key]
        public int CM_ID { get; set; }
        [Display(Name = "Name")]
        public string CM_Name { get; set; }
        public string CM_Address { get; set; }
        public string CM_Job { get; set; }
        [Display(Name = "Phone")]
        public int CM_Phone { get; set; }
    }
}

