using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;
using System.Web.Http;
using Shop.WebApi.Services;

namespace Shop.WebApi.DependencyInjection
{
    public class DependencyInjectionConfiguration
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}
