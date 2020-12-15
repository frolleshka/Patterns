namespace FactoryMethod.PatternWithDI
{

    public sealed class SecondCarRunner : TransportRunner<ITransport>
    {
        private readonly Car _car;

        public SecondCarRunner(Car car)
        {
            _car = car;
        }

        protected override ITransport GetTransport() => _car;
    }

    public sealed class CarRunner : TransportRunner<ITransport>
    {
        protected override ITransport GetTransport() => new Car();
    }
}
