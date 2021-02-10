using Bridge.Claassic;
using Bridge.WithDI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace Bridge
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
