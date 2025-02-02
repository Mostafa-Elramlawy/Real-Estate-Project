using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public DateTime Bid_End { get; set; }
        public DateTime Bid_start { get; set; }
        public string Bid_Name { get; set; }

        [Display(Name = "Start Price:")]
        public int Start_price { get; set; }

        [Display(Name = "Lowest Bid:")]
        public int lowest_bidding_price { get; set; }
        public int AStatus_ID { get; set; }
        public Property Property { get; set; }

        public int Pro_id { get; set; }
        public int UserID { get; set; }
    }
}
