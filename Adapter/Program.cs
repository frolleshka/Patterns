using Adapter.Classic;
using Adapter.WithDI;
using System;
using System.Collections.Generic;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassicRunner.Run();
            DiRunner.Run();
        }
}
}
