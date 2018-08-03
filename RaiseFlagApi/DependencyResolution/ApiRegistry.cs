using RaiseFlag.ServiceLayer.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaiseFlagApi.DependencyResolution
{
    public class ApiRegistry :Registry
    {
        public ApiRegistry()
        {
            Scan(
              scan =>
              {
                  scan.TheCallingAssembly();
                  scan.WithDefaultConventions();
                  scan.With(new ControllerConvention());
              });
            IncludeRegistry<CustomApiRegistry>();
            IncludeRegistry<ServiceLayerRegistry>();
        }

    }
}