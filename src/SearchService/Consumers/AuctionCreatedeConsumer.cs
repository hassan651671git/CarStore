using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionCreatedeConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;

        public AuctionCreatedeConsumer(IMapper mapper){
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine("-------> Consuming new Auction "+context.Message.Id);
            var item=_mapper.Map<Item>(context.Message);

            if(item.Model=="Foo")
            {
                throw new ArgumentException("Cannot sell cars with mode Foo!");
            }

            Console.WriteLine("###############"+item.Model);

            await item.SaveAsync();
        }

    }
}