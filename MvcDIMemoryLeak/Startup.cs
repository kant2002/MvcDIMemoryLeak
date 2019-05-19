using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Mvc;
using MvcDIMemoryLeak.Models;

[assembly: OwinStartup(typeof(MvcDIMemoryLeak.Startup))]

namespace MvcDIMemoryLeak
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DummyService>();
            services.AddTransient(typeof(Controllers.HomeController));
        }
    }
}
