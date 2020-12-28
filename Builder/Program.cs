using Builder.Classic;
using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassicRunner.Run();
            Console.WriteLine("==========");
            ConcreteFluentRunner.Run();
        }
    }
}
