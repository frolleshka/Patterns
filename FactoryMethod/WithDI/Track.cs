using System;

namespace FactoryMethod.PatternWithDI
{
    public class Track : ITransport
    {
        public void ConcreteRun()
        {
            Console.WriteLine("Track is Run");
        }
    }
}
