using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace Biddingservice_controllers.Models
{
    public class Auction : Entity
    {
        public DateTime AuctionEnd { get; set; }
        public string Seller { get; set; }
        public bool Finished { get; set; }
        public int ReservedPrice { get; set; }
    }
}