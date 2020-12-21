using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.WithDI
{
    public static class DiRunner
    {
        public static void Run()
        {
            Console.WriteLine("===DiRunner===");

            var container = Composite.Compose();
            var human = container.Get<IHuman>();
            human.WhatMood();
        }
    }

    public static class Composite
    {
        public static IKernel Compose()
        {
            var kernel = new StandardKernel();
            var IsNegative = new Random().Next(0, 1) == 1;
            if (IsNegative)
            {
                kernel.Bind<IPersonFactory>().To<NegativePersonFactory>().InSingletonScope();
            }
            else
            {
                kernel.Bind<IPersonFactory>().To<PositivePersonFactory>().InSingletonScope();
            }

            kernel.Bind<IHuman>().ToMethod(c => c.Kernel.Get<IPersonFactory>().CreateHuman());
            return kernel;
        }
    }


    public interface IHuman
    {
        public void WhatMood();
    }

    public class NagativeHuman : IHuman
    {
        public void WhatMood()
        {
            Console.WriteLine("Human is Angry");
        }
    }

    public class PositiveHuman : IHuman
    {
        public void WhatMood()
        {
            Console.WriteLine("Human is Kind");
        }
    }

    public interface IPersonFactory
    {
        public IHuman CreateHuman();
    }

    public class PositivePersonFactory : IPersonFactory
    {
        private readonly IKernel _kernel;

        public PositivePersonFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHuman CreateHuman()
        {
            Console.WriteLine("PositivePersonFactory create human");
            return _kernel.Get<PositiveHuman>();
        }
    }

    public class NegativePersonFactory : IPersonFactory
    {
        private readonly IKernel _kernel;

        public NegativePersonFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHuman CreateHuman()
        {
            Console.WriteLine("NegativePersonFactory create human");
            return _kernel.Get<NagativeHuman>();
        }
    }
}
