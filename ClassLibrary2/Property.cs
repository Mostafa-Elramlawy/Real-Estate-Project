using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary2
{
    public class Property
    {
        [Key]
        public int Pro_id { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NumberOfBedRooms { get; set; }
        public string NumberOfBathRooms { get; set; }
        public string NumOfLivingRoom { get; set; }
        public string P3D_path { get; set; }
        public int Floor_Num { get; set; }
        public int size { get; set; }
        public int garage_num { get; set; }
        public int Building_Num { get; set; }
        public int Status_ID { get; set; }
        public int P_Type_ID { get; set; }
        public int City_id { get; set; }
        public int Government_id { get; set; }
        public int District_ID { get; set; }
        public int UserID { get; set; }
        public string ImagePath { get; set; }
        public virtual User User { get; set; }
    }
}
