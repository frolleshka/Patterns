using FactoryMethod.PatternWithDI;
using FactoryMethod.WithDI;
using System;
using Ninject;
using System.Collections.Generic;
using FactoryMethod.Abstract;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====DI====");
            var container = Composition.GetContainer();
            foreach (var transport in container.Get<IEnumerable<ITransportRunner<ITransport>>>())
            {
                transport.Run();
            }
            var bike = container.Get<ITransportRunner<Bike>>();
            bike.Run();

            var track = container.Get<ITransportRunner<Track>>();
            track.Run();

            Console.WriteLine();

            Console.WriteLine("====CLASSIC====");
            var middlePortal = new MiddlePortal();
            middlePortal.LenInMonster();

            var smallPortal = new SmallPortal();
            smallPortal.LenInMonster();
        }
    }
}
