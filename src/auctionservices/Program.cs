using AuctionService;
using AuctionService.Data;
using AuctionService.RequestHelpers;
using auctionservices.Data;
using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AuctionDbContext>(
    opt=>opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(config=>{
    config.AddEntityFrameworkOutbox<AuctionDbContext>(o=>
    {
        o.QueryDelay=TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
     });

     config.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
     config.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction",false));
     config.UsingRabbitMq((context,cfg)=>{
        cfg.ConfigureEndpoints(context);
    });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });

var app = builder.Build();
DbInitializer.InitDb(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
