using System;

namespace FactoryMethod.PatternWithDI
{
    public class Bike : ITransport
    {
        public void ConcreteRun()
        {
            Console.WriteLine("Bike is Run");
        }
    }
}
