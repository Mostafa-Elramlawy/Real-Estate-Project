using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AuctionContext
    {
        public DbSet<Auction> Auctions { get; set; }

    }
}
