using AuctionService.Data;
using AuctionService.RequestHelpers;
using auctionservices.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AuctionDbContext>(
    opt=>opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
    
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
DbInitializer.InitDb(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
