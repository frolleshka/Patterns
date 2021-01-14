using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class Sharp9_ModuleInitializer
    {
        private static Sharp9_ModuleInitializer InstanceHolder;
        private Sharp9_ModuleInitializer() { }

        [ModuleInitializer]
        internal static void Init()
        {
            InstanceHolder = InstanceHolder ?? new Sharp9_ModuleInitializer();
        }

        public Sharp9_ModuleInitializer Instance => InstanceHolder;
    }
}
