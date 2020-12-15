namespace FactoryMethod.PatternWithDI
{
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
