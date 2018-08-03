using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaiseFlagApi.DependencyResolution
{
    public static class IoC
    {
        internal const string CacheKey = "RaiseFlagDependencyContainer";
        private static IContainer _container;
        public static IContainer PermenantContainer => _container ?? (_container = Initialize());
        public static IContainer Initialize()
        {
            return new Container(c => c.AddRegistry<ApiRegistry>());

        }

        public static IContainer Container => HttpContext.Current == null ? PermenantContainer.GetNestedContainer() : GetContextFromContainer();

        private static IContainer GetContextFromContainer()
        {
            var currentContext = HttpContext.Current;
            var container = currentContext.Items[CacheKey] as IContainer;
            if (container == null)
                currentContext.Items[CacheKey] = container = PermenantContainer.GetNestedContainer();
            return container;
        }
    }

}