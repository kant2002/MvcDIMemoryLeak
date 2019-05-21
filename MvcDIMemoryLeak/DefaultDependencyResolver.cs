using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MvcDIMemoryLeak
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider ServiceProvider;

        public DefaultDependencyResolver(IServiceCollection serviceProvider)
        {
            serviceProvider.AddSingleton<IControllerFactory, ScopedControllerFactory>(_ => new ScopedControllerFactory(this));

            ServiceProvider = serviceProvider.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
        {
            var service = ServiceProvider.GetService(serviceType);
            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType);
        }

        class ScopedControllerFactory : DefaultControllerFactory
        {
            private DefaultDependencyResolver parentResolver;

            public ScopedControllerFactory(DefaultDependencyResolver defaultDependencyResolver)
            {
                this.parentResolver = defaultDependencyResolver;
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    return base.GetControllerInstance(requestContext, controllerType);
                }

                var scope = this.parentResolver.ServiceProvider.CreateScope();
                requestContext.HttpContext.Items["DiScope"] = scope;
                return (IController)scope.ServiceProvider.GetService(controllerType);
            }

            public override void ReleaseController(IController controller)
            {
                var mvcController = controller as Controller;
                if (mvcController != null)
                {
                    var scope = (IServiceScope)mvcController.HttpContext.Items["DiScope"];
                    scope.Dispose();
                }

                base.ReleaseController(controller);
            }
        }
    }
}