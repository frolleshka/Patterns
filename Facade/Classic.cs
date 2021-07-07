using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    public class Classic
    {
        public static void Run()
        {
            var lib1 = new Library1();
            var lib2 = new Library2();
            var facade = new Facade(lib1, lib2);
            Console.WriteLine(facade.Operation());
        }
    }

    public class Library1
    {
        public string Operation()
        {
            return "Operation Library1";
        }
    }

    public class Library2
    {
        public string Operation()
        {
            return "Operation Library2";
        }
    }

    public class Facade
    {
        private readonly Library1 library1;
        private readonly Library2 library2;

        public Facade(Library1 library1, Library2 library2)
        {
            this.library1 = library1;
            this.library2 = library2;
        }

        public string Operation()
        {
            return library1.Operation() + Environment.NewLine + library2.Operation();
        }
    }
}
