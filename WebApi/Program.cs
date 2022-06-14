using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.ExtensionMethods;
using WebApi.Profiles;

var builder = WebApplication.CreateBuilder(args);


// Add Configuration
ConfigurationManager Configuration = builder.Configuration; //

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MarketDbContext>(opt =>
{
    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

//using(var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<MarketDbContext>();
//    db.Database.Migrate();
//}

await app.ApplyMigrationsAsync();

app.UseAuthorization();

app.MapControllers();

app.Run();


//static void ApplyMigrations(WebApplication app)
//{
//    using var scope = app.Services.CreateScope();
//    var db = scope.ServiceProvider.GetRequiredService<MarketDbContext>();
//    db.Database.Migrate();
//}

