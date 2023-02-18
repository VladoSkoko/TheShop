using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;
using System.Web.Http;
using Autofac.Extras.NLog;

namespace Shop.WebApi.DependencyInjection
{
    public class DependencyInjectionConfiguration
    {
        public static IContainer Container;

        public static void Configure(HttpConfiguration config)
        {
            Configure(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Configure(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            Bootstrapper.RegisterDependencies(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<NLogModule>();

            Container = builder.Build();

            return Container;
        }
    }
}
