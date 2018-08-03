using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaiseFlag.BLL.DependencyResolution
{
    public class BLLRegistry : Registry
    {
        public BLLRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            IncludeRegistry<CustomBllRegistry>();
        }
    }
}
