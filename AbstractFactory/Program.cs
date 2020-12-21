using AbstractFactory.Classic;
using AbstractFactory.WithDI;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassicRunner.Run();
            Console.WriteLine("==========");
            DiRunner.Run();
        }
    }
}
