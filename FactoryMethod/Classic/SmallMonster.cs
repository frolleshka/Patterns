using System;

namespace FactoryMethod.Abstract
{
    public sealed class SmallMonster : IMonster
    {
        public void Scream()
        {
            Console.WriteLine("Small monster scream: aaaarrr");
        }
    }
}
