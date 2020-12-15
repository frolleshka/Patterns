using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.PatternWithDI
{
    public abstract class TransportRunner<T> : ITransportRunner<T> 
        where T : ITransport
    {
        public void Run()
        {
            var transport = GetTransport();
            transport.ConcreteRun();
            Console.WriteLine("=========");
        }

        protected abstract T GetTransport();
    }
}
