using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassicRunner.Run();
            WithDI.Run();
        }
    }
}
