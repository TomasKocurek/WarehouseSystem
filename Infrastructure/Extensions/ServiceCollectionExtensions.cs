using Infrastructure.Persistence;
using Infrastructure.Repositories.MovementRepositories;
using Infrastructure.Repositories.OrderRepositories;
using Infrastructure.Repositories.ProductRepositories;
using Infrastructure.Repositories.StockItemRepositories;
using Infrastructure.Repositories.StockRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseDbContext>(options =>
        {
            options
            .UseMySql(configuration.GetConnectionString("WarehouseDB"),
                new MariaDbServerVersion(new Version(10, 9, 3)))
            .UseBatchEF_MySQLPomelo();
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockItemRepository, StockItemRepository>();
        services.AddScoped<IMovementRepository, MovementRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
