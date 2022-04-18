using Microsoft.EntityFrameworkCore;

namespace SuperStore.Carts.DAL;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<CartsDbContext>(x =>
            x.UseNpgsql("Host=localhost;Database=carts.service;Username=postgres;Password="));
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        return services;
    }
}