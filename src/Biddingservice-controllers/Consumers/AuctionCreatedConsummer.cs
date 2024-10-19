using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biddingservice_controllers.Models;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace Biddingservice_controllers.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            var auction = new Auction()
            {
                ID = context.Message.Id.ToString(),
                AuctionEnd = context.Message.AuctionEnd,
                Seller = context.Message.Seller,
                ReservedPrice = context.Message.ReservePrice

            };

            await auction.SaveAsync();
        }
    }
}