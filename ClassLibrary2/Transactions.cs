using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Transactions
    {
        [Key]
        public int Transaction_ID { get; set; }
        public int Pro_Price { get; set; }
        public DateTime Transaction_date { get; set; }
        public int AuctionID { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; } // New property for username
        public string Title { get; set; }
        public string NumberOfBedRooms { get; set; }
        public string NumberOfBathRooms { get; set; }
        public string NumOfLivingRoom { get; set; }

        public string ImagePath { get; set; }
    }
}
