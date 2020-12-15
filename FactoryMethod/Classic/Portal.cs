using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Abstract
{
    public abstract class Portal
    {
        public void ShockMonster()
        {
            var monster = CreateMonster();
            monster.Scream();
            Console.WriteLine("***********");
        }

        protected abstract IMonster CreateMonster();
    }
}
