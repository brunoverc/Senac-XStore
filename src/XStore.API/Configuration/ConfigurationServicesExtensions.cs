using Microsoft.EntityFrameworkCore;
using XStore.Infra.Data.Context;

namespace XStore.API.Configuration
{
    public static class ConfigurationServicesExtensions
    {
        public static IServiceCollection DbContextConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<XStoreDbContext>(options => options.UseSqlServer(connectionString));

            services.AddDistributedMemoryCache();

            return services;
        }
    }
}
