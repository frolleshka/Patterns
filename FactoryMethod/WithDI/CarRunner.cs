namespace FactoryMethod.PatternWithDI
{

    public sealed class DoubleCarRunner : TransportRunner<ITransport>
    {
        private readonly Car _car;

        public DoubleCarRunner(Car car)
        {
            _car = car;
        }

        protected override ITransport GetTransport() => _car;
    }

    public sealed class CarRunner : TransportRunner<ITransport>
    {
        private readonly Car _car;

        public CarRunner(Car car)
        {
            _car = car;
        }

        protected override ITransport GetTransport() => _car;
    }
}
