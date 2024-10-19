using AuctionServices.Data;
using Contracts;
using MassTransit;
using AuctionServices.Entities;
namespace AuctionServices.Consummers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
    {
        private readonly AuctionDbContext _auctionDbContext;

        public AuctionFinishedConsumer(AuctionDbContext auctionDbContext)
        {
            _auctionDbContext = auctionDbContext;
        }

        public async Task Consume(ConsumeContext<AuctionFinished> context)
        {
            var auction = await _auctionDbContext.Auctions.FindAsync(context.Message.AuctionId);

            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.ReservePrice = context.Message.Amount ?? -1;
            }

            auction.Status = auction.SoldAmount > auction.ReservePrice
            ? Entities.Status.Finished : Entities.Status.ReserveNotMet;
            await _auctionDbContext.SaveChangesAsync();
        }
    }
}