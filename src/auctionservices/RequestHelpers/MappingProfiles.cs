﻿using AuctionService.DTOs;
using auctionservices.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
        CreateMap<AuctionDto, CreateAuctionDto>();
        CreateMap<Auction, UpdateAuctionDto>().IncludeMembers(a => a.Item);
        CreateMap<Item, UpdateAuctionDto>();
        CreateMap<AuctionDto,AuctionCreated>();    
        CreateMap<Auction, AuctionUpdated>().IncludeMembers(a => a.Item);
        CreateMap<Item, AuctionUpdated>();

    }
}
