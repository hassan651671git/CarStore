using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auctionservices;
using Biddingservice_controllers.Models;
using Grpc.Net.Client;

namespace Biddingservice_controllers.Services
{
    public class GrpcAuctionClient
    {
        private readonly ILogger<GrpcAuctionClient> _logger;
        private readonly IConfiguration _configuration;

        public GrpcAuctionClient(ILogger<GrpcAuctionClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Auction GetAuction(string id)
        {
            _logger.LogInformation("Calling Grpc Service");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcAuction"]);
            var clinet = new GrpcAution.GrpcAutionClient(channel);
            var request = new GetAuctionRequest { Id = id };

            try
            {
                var replay = clinet.GetAuction(request);

                var auction = new Auction
                {
                    ID = replay.Auction.Id,
                    AuctionEnd = DateTime.Parse(replay.Auction.AuctionEnd),
                    ReservedPrice = replay.Auction.ReservePrice,
                    Seller = replay.Auction.Seller
                };

                return auction;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Couldn't call Grpc!");
                return null;
            }



        }


    }
}