using EFCoreRepositoryUnitofWork.Interfaces;
using EFCoreRepositoryUnitofWork.Persistence;
using EFCoreRepositoryUnitofWork.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());

        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddTransient(typeof(IReadRepository<>), typeof(BaseRepository<>));
        services.AddTransient<ICustomPostRepository, CustomPostRepository>();

        return services;
    }
}
