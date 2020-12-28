using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Classic
{
    public static class ClassicRunner
    {
        public static void Run()
        {
            var modernDirector = new ModernChearDirector();
            var modernChear = modernDirector.BuildChear(new ChearBuilder());
            Console.WriteLine(modernChear);
            var classicDirector = new ClassicChearDirector(new ChearBuilder());
            Console.WriteLine(classicDirector.BuildChear());
            Console.WriteLine(classicDirector.BuildChear());
        }
    }

    public class Thing
    {
        private List<string> Parts = new List<string>();
        private string ThingName { get; }

        public Thing(string thingName)
        {
            ThingName = thingName;
        }

        public void AddPath(string part)
        {
            Parts.Add(part);
        }

        public override string ToString()
        {
            return $"Thing [{ThingName}] consists of " + string.Join(", ", Parts);
        }
    }

    public class ModernChearDirector
    {
        public Thing BuildChear(ChearBuilder chearBuilder)
        {
            for(var i = 0; i < 3; i++)
            {
                chearBuilder.AddLeg();
            }
            chearBuilder.AddВackrest();
            chearBuilder.AddВackrest();
            chearBuilder.AddDesign("Modern lines");
            return chearBuilder.Build();
        }
    }

    public class ClassicChearDirector
    {
        private readonly ChearBuilder _chearBuilder;
        public ClassicChearDirector(ChearBuilder chearBuilder)
        {
            _chearBuilder = chearBuilder;
        }

        public Thing BuildChear()
        {
            for (var i = 0; i < 4; i++)
            {
                _chearBuilder.AddLeg();
            }
            _chearBuilder.AddВackrest();
            _chearBuilder.AddDesign("Classic borokko");
            return _chearBuilder.Build();
        }
    }

    public class ChearBuilder
    {
        private Thing Thing { get; set; }
        public ChearBuilder()
        {
            Reset();
        }

        public void AddLeg()
        {
            Thing.AddPath("Leg");
        }

        public void AddВackrest()
        {
            Thing.AddPath("Вackrest");
        }

        public void AddDesign(string design)
        {
            Thing.AddPath($"design[{design}]");
        }

        public Thing Build()
        {
            var result = Thing;
            Reset();
            return result;
        }

        public void Reset()
        {
            Thing = new Thing("Chear");
        }
    }
}
