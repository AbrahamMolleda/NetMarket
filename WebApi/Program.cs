using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Configuration
ConfigurationManager Configuration = builder.Configuration; //

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MarketDbContext>(opt =>
{
    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
})
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
