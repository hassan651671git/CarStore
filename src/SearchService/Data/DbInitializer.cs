using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(WebApplication app)
        {


            await DB.InitAsync("serachDB",
            MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("mongoDbConnection")));

            await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

            var count = await DB.CountAsync<Item>();

             using var scop=app.Services.CreateScope();
             var httpAuction=scop.ServiceProvider.GetRequiredService<AuctionSvcHttpClient>();
             var items=await httpAuction.GetItemsForSearchDb();

             if(items.Count() > 0)
             {
                Console.WriteLine("Getting "+items.Count()+"items from auction service");
                await  DB.SaveAsync(items);
             }


        }
    }
}