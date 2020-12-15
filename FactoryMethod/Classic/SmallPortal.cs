namespace FactoryMethod.Abstract
{
    public class SmallPortal : Portal
    {
        protected override IMonster CreateMonster()
        {
            return new SmallMonster();
        }
    }
}
