using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AuctionViewModel
    {
        public int Pro_Price { get; set; }
        public int AuctionID { get; set; }
        public string Bid_Name { get; set; }
        public int Start_price { get; set; }
        public DateTime Bid_start { get; set; }
        public DateTime Bid_End { get; set; }
        public int lowest_bidding_price { get; set; }
        public int Pro_id { get; set; }
        public int AStatus_ID { get; set; }

        public string ImagePath { get; set; }
        public Auction auction { get; set; }

        public string AStatus_name { get; set; }
        public Property Property { get; set; }
        public List<Property> PropertyList { get; set; }
        public int UserID { get; set; }
    }
}
