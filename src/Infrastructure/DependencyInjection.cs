using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

        services.AddTransient(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
        services.AddTransient(typeof(IReadRepositoryBase<>), typeof(BaseRepository<>));
        services.AddTransient<ITodoListRepository, TodoListRepository>();

        return services;
    }
}
