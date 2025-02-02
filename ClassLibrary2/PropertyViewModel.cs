using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassLibrary2
{
    public class PropertyViewModel
    {
        public Property Property { get; set; }


        public int City_id { get; set; }
        public string City_name { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public int Status_ID { get; set; }
        public string Status_Name { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public int Government_id { get; set; }
        public string Government_name { get; set; }
        public IEnumerable<Government> Governments { get; set; }
        public int P_Type_ID { get; set; }
        public string P_Type_Name { get; set; }
        public IEnumerable<P_Type> P_Types { get; set; }
        public int District_ID { get; set; }
        public string District_name { get; set; }
        public IEnumerable<District> Districts { get; set; }

        public string P3D_path { get; set; }
        public Property_img PropertyImage { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string ImagePath { get; set; }
        public IEnumerable<Property_img> PropertyImages { get; set; } // Updated type
        public List<Property> Properties { get; set; }
        public User User { get; set; }
    }
}