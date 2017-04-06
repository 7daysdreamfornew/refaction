using Microsoft.Practices.Unity;
using refactor_me.Resolver;
using refactor_me.Services;
using System.Web.Http;

namespace refactor_me
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
           var container = new UnityContainer();
            // Register ProductsService and ProductOptionsService repositories
            container.RegisterType<IProductsService, ProductsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptionsService, ProductOptionsService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
