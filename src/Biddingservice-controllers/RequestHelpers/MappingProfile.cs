using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BiddingService;
using Contracts;

namespace Biddingservice_controllers.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BidDto, Bid>().ReverseMap();
            CreateMap<Bid, BidPlaced>().ReverseMap();
        }

    }
}