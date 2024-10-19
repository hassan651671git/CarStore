using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionServices.Entities;
using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
namespace AuctionServices.Data
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Auction>()
            .HasOne(auction => auction.Item)
            .WithOne(item => item.Auction)
            .HasForeignKey<Item>(item => item.AuctionId);

            builder.AddInboxStateEntity();
            builder.AddOutboxMessageEntity();
            builder.AddOutboxStateEntity();
        }

        public DbSet<Auction> Auctions { get; set; }
    }
}