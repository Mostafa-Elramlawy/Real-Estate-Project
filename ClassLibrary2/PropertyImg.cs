using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Property_img
    {
        [Key]
        public int img_id { get; set; }
        public string img_name { get; set; }
        public string img_path { get; set; }
        public int Pro_id { get; set; }
    }
}
