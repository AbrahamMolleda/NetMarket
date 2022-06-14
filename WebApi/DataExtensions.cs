using BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods
{
    public static class DataExtensions
    {
        public static async Task<WebApplication> ApplyMigrationsAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<MarketDbContext>();
                await context.Database.MigrateAsync();
                
                if (app.Environment.IsDevelopment())
                    await MarketDbContextData.CargarDataPruebaAsync(context, loggerFactory);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message, "Errores en el proceso de Migracion");
            }
            return app;
        }
    }
}
