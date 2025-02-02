using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary2
{
    public class TransactionsViewModel
    {
        public Transactions transactions { get; set; }
        public int HighestPrice { get; set; }
        public Auction Auction { get; set; }
        public Property Property { get; set; }
        public TimeSpan TimeRemaining { get; set; }
        public int lowest_bidding_price { get; set; }

        public string ImagePath { get; set; }

    }
}