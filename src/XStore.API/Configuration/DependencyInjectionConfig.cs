using MediatR;
using XStore.Application.Interfaces;
using XStore.Application.Services;
using XStore.Domain.Interfaces;
using XStore.Domain.Shared.Transaction;
using XStore.Infra.Data.Context;
using XStore.Infra.Data.Repositories;
using XStore.Infra.Data.UoW;

namespace XStore.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatr();
            services.AddRepositories();
            services.AddServices();
        }

        public static void AddMediatr(this IServiceCollection services)
        {
            //Você deve adicionar para todos os projetos
            services.AddMediatR(AppDomain.CurrentDomain.Load("XStore.Domain"));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            //Você deve adicionar para todos os projetos
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<XStoreDbContext>();

            //Altera para suas classes de repositório
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAddressAppService, AddressAppService>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IVoucherAppService, VoucherAppService>();
        }
    }
}
