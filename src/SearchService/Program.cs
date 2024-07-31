
using System.Net;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{



}

app.UseHttpsRedirection();
app.MapControllers();



app.Run();

app.Lifetime.ApplicationStarted.Register(async ()=> await DbInitializer.Initialize(app));




static IAsyncPolicy<HttpResponseMessage> GetPolicy()=>
          HttpPolicyExtensions.HandleTransientHttpError()
                  .OrResult(o=>o.StatusCode==HttpStatusCode.NotFound)
                  .WaitAndRetryForeverAsync(_=>TimeSpan.FromSeconds(3));