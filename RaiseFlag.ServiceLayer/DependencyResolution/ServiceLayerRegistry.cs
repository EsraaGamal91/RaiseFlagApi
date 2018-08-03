using RaiseFlag.BLL.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.ServiceLayer.DependencyResolution
{
    public class ServiceLayerRegistry : Registry
    {
        public ServiceLayerRegistry()
        {
            Scan(
               scan =>
               {
                   scan.TheCallingAssembly();
                   scan.WithDefaultConventions();
               });
            IncludeRegistry<CustomServiceLayerRegistry>();
            IncludeRegistry<BLLRegistry>();
        }
    }
}
