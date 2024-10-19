using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionServices.Data;
using Grpc.Core;

namespace auctionservices
{
    public class GrpcAuctionService : GrpcAution.GrpcAutionBase
    {
        private readonly AuctionDbContext _auctionDbContext;

        public GrpcAuctionService(AuctionDbContext auctionDbContext)
        {
            _auctionDbContext = auctionDbContext;
        }

        public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
        {
            Console.WriteLine("====> recieved GRPC request from auction");

            var auction = await _auctionDbContext.Auctions.FindAsync(Guid.Parse(request.Id))
             ?? throw new RpcException(new Status(StatusCode.NotFound, "Not found"));

            var response = new GrpcAuctionResponse
            {
                Auction = new GrpcAuctionModel
                {
                    AuctionEnd = auction.AuctionEnd.ToString(),
                    Id = auction.Id.ToString(),
                    ReservePrice = auction.ReservePrice,
                    Seller = auction.Seller
                }
            };

            return response;

        }
    }
}