using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;


namespace RaiseFlagApi.DependencyResolution
{
    public class Resolver : IDependencyResolver
    {
        readonly IContainer _container;

        public Resolver(IContainer container)
        {
            _container = container;

            // TODO: if you haven’t registered necessary interfaces somewhere else, you’ll need to do so here.


        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsClass)
            {
                return GetConcreteService(serviceType);
            }
            else
            {
                return GetInterfaceService(serviceType);
            }
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                // Can’t use TryGetInstance here because it won’t create concrete types
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new Resolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }

}