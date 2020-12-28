using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Classic
{
    public static class ConcreteFluentRunner
    {
        public static void Run()
        {
            var modernDirector = new UserFluentBuilder()
                .SetName("Vasia")
                .SetSorname("Loshkin")
                .SetAddress(city: "SPB", "Lenina", -12)
                .Build();
            Console.WriteLine(modernDirector);
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Sorname { get; set; }
        public string Address { get; set; }
        public override string ToString()
        {
            return $"{Name} {Sorname} {Address}";
        }
    }

    public class UserFluentBuilder
    {
        private User _instance;

        public UserFluentBuilder()
        {
            Reset();
        }

        public UserFluentBuilder SetName(string name)
        {
            _instance.Name = name;
            return this;
        }

        public UserFluentBuilder SetSorname(string sorname)
        {
            _instance.Sorname = sorname;
            return this;
        }


        public UserFluentBuilder SetAddress(string city, string street, int homeNumber)
        {
            // В билдере может быть любая скрытая от клиента логика
            // его задача создать согласованный "правильный" объект,
            // Можно выкинуть исключение при валидации, можно вернуть ошибку при Build

            _instance.Address = $"city [{city}] ; street[{street}_{Math.Abs(homeNumber)}]";
            return this;
        }

        public User Build()
        {
            var result = _instance;
            Reset();
            return result;
        }

        private void Reset()
        {
            _instance = new User();
        }
    }
}
