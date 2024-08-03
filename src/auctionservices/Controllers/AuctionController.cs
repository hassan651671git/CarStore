using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AuctionService.DTOs;
using auctionservices.Data;
using auctionservices.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auctionservices.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionDbContext _auctionDbContext;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuctionController(AuctionDbContext auctionDbContext, IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _auctionDbContext = auctionDbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
        {
            var query = _auctionDbContext.Auctions.OrderBy(x => x.Item.Make).AsQueryable();
            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
            var auction = await _auctionDbContext.Auctions.Include(a => a.Item).OrderBy(a => a.Item.Make)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (auction == null)
            {
                return NotFound("didn't found");
            }

            var auctionDto = _mapper.Map<AuctionDto>(auction);
            return auctionDto;

        }

        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
        {
            var auction = _mapper.Map<Auction>(createAuctionDto);
           // auction.Seller = "test seller";
            await _auctionDbContext.Auctions.AddAsync(auction);

           var auctionDto=_mapper.Map<AuctionDto>(auction);
           await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(auctionDto));
           var result = await _auctionDbContext.SaveChangesAsync();

            if (result == 0)
            {
                return BadRequest("Couldn't save to database!");
            }

            return CreatedAtAction(nameof(this.GetAuctionById), new { auction.Id }, auctionDto);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
            var auction = await _auctionDbContext.Auctions.Include(a => a.Item).FirstOrDefaultAsync(a => a.Id == id);

            if (auction == null) return NotFound();

            //  if (auction.Seller != User.Identity.Name) return Forbid();

            auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
            auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
            auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
            auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

            await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

            var result = await _auctionDbContext.SaveChangesAsync();

            if (result > 0) return Ok();

            return BadRequest("Problem saving changes");



        }




        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var auction = await _auctionDbContext.Auctions.FirstOrDefaultAsync(a => a.Id == id);
            if (auction == null)
            {
                return NotFound();
            }

            _auctionDbContext.Auctions.Remove(auction);
            await _publishEndpoint.Publish<AuctionDeleted>(new { Id = auction.Id.ToString() });
            var result = await _auctionDbContext.SaveChangesAsync();
            
            if (result > 0) return Ok();
            return BadRequest("Can't delete");

        }
    }
}