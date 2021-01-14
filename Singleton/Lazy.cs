using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    /// <summary>
    /// Lazy and tread save singletone
    /// </summary>
    public class LazySingleton
    {
        private static readonly Lazy<LazySingleton> InstanceHolder = new Lazy<LazySingleton>(() => new LazySingleton());
        
        private LazySingleton()
        {
        }

        public static LazySingleton Instance => InstanceHolder.Value;
    }
}
