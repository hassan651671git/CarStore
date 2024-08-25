
using System.Net;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());

builder.Services.AddMassTransit(config =>
{

    config.AddConsumersFromNamespaceContaining<AuctionCreatedeConsumer>();
    config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    config.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
        host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
        host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });
        
        cfg.ReceiveEndpoint("search-auction-created",
        e =>
        {
            e.UseMessageRetry(r => r.Interval(5, 5));
            e.ConfigureConsumer<AuctionCreatedeConsumer>(context);
        });


        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{



}

app.UseHttpsRedirection();
app.MapControllers();


app.Lifetime.ApplicationStarted.Register(async () => await DbInitializer.Initialize(app));
app.Run();






static IAsyncPolicy<HttpResponseMessage> GetPolicy() =>
          HttpPolicyExtensions.HandleTransientHttpError()
                  .OrResult(o => o.StatusCode == HttpStatusCode.NotFound)
                  .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));