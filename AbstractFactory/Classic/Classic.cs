using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Classic
{
    public static class ClassicRunner
    {
        public static void Run()
        {
            Console.WriteLine("===Classic===");
            
            // Конкретная фабрика обычно выьерается исходя из некоторых условий
            // или состояний нп платформа, время года, ключ в конфиге и тд...
            PutWear(new SummerClothesFactory());
            PutWear(new WinterClothesFactory());

            Console.WriteLine("=============");
            Console.WriteLine();
        }

        private static void PutWear(IAbstractFactory clothesFactory)
        {
            clothesFactory.CreateFootwear().PutFootwear();
            Console.WriteLine();
        }
    }

    public interface IFootwear
    {
        public void PutFootwear();
    }

    public class Snickers : IFootwear
    {
        public void PutFootwear()
        {
            Console.WriteLine("You put snickers");
        }
    }

    public class Boots : IFootwear
    {
        public void PutFootwear()
        {
            Console.WriteLine("You put Boots");
        }
    }

    public class WinterClothesFactory : IAbstractFactory
    {
        public IFootwear CreateFootwear()
        {
            return new Boots();
        }
    }

    public class SummerClothesFactory : IAbstractFactory
    {
        public IFootwear CreateFootwear()
        {
            return new Snickers();
        }
    }

    public interface IAbstractFactory
    {
        /*
         Может быть полно всяких сущностей которые надо создать.
         */
        IFootwear CreateFootwear();
    }
}
