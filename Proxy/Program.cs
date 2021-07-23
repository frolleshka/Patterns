using System;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Classic.Run();
            Console.WriteLine("====");
            WithDI.Run();
        }
    }
}
