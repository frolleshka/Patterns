using System;

namespace FactoryMethod.PatternWithDI
{
    public class Car : ITransport
    {
        public void ConcreteRun()
        {
            Console.WriteLine("Car is Run");
        }
    }
}
