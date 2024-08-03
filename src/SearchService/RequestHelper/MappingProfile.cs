using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Item,AuctionCreated>().ReverseMap();
            CreateMap<AuctionUpdated, Item>();

        }

    }
}