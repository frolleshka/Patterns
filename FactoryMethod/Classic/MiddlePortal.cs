namespace FactoryMethod.Abstract
{
    public sealed class MiddlePortal : Portal
    {
        protected override IMonster CreateMonster()
        {
            return new MiddleMonster();
        }
    }
}
