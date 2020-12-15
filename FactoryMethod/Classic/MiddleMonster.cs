using System;

namespace FactoryMethod.Abstract
{
    public class MiddleMonster : IMonster
    {
        public void Scream()
        {
            Console.WriteLine("Middle monster scream: AAAARRRR");
        }
    }
}
