using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {
        public static async Task CargarDataPruebaAsync(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Marcas.Any())
                {
                    var marcaData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");
                    var marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData);
                    
                    foreach(var marca in marcas)
                    {
                        await context.Marcas.AddAsync(marca);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Categorias.Any())
                {
                    var categoriaData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");
                    var categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    foreach (var categoria in categorias)
                    {
                        await context.Categorias.AddAsync(categoria);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Productos.Any())
                {
                    var productoData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");
                    var productos = JsonSerializer.Deserialize<List<Producto>>(productoData);

                    foreach (var producto in productos)
                    {
                        await context.Productos.AddAsync(producto);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<MarketDbContextData>();
                logger.LogError(ex.Message);
            }
        }
    }
}
