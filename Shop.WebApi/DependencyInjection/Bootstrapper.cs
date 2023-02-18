using Autofac;
using Shop.WebApi.Articles.Repositories;
using Shop.WebApi.Configuration;
using Shop.WebApi.Database;
using Shop.WebApi.Suppliers.Managers;
using Shop.WebApi.Suppliers.Services;

namespace Shop.WebApi.DependencyInjection
{
    public class Bootstrapper
    {
        public static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<AppConfiguration>().As<IAppConfiguration>();

            builder.RegisterType<ArticleRepository>().As<IArticleRepository>();
            
            builder.RegisterType<Db>().As<IDb>();

            builder.RegisterType<CachedSupplierService>().As<ICachedSupplierService>();
            builder.RegisterType<WarehouseSupplierService>().As<ISupplierService>();
            builder.RegisterType<Dealer1SupplierService>().As<ISupplierService>();
            builder.RegisterType<Dealer2SupplierService>().As<ISupplierService>();
            builder.RegisterType<SupplierManager>().As<ISupplierManager>();
        }
    }
}
