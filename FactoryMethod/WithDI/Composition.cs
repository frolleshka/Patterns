using FactoryMethod.PatternWithDI;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.WithDI
{
    public static class Composition
    {
        public static IKernel GetContainer()
        {
            var kernel = new StandardKernel();

            kernel.Bind<ITransportRunner<Track>>().To<GenericTransportRunner<Track>>();

            kernel.Bind<ITransportRunner<ITransport>>().To<CarRunner>();
            kernel.Bind<ITransportRunner<ITransport>>().To<DoubleCarRunner>();

            kernel.Bind<ITransportRunner<Bike>>().To<BikeRunner>();

            kernel.Bind<ITransport>().To<Car>();
            kernel.Bind<ITransport>().To<Bike>();
            kernel.Bind<ITransport>().To<Track>();
            // TODO REgistrate assembly

            return kernel;
        }
    }
}
