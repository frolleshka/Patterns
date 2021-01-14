using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singleton
{
    public class ClassicThreadSave
    {
        private static readonly object _lock_ = new object();
        private static ClassicThreadSave instance;

        private ClassicThreadSave()
        {
        }

        public static ClassicThreadSave Instance
        { 
            get
            {
                if(instance != null)
                {
                    return instance;
                }

                Monitor.Enter(_lock_);
                var temp = new ClassicThreadSave();
                // очень честный синглтон, можно заюзать Interlocked.Exchange
                // тогда в instance всегда будет записан temp 
                Interlocked.CompareExchange(ref instance, temp, null);
                Monitor.Exit(_lock_);
                return instance;
            }
        }
    }
}
