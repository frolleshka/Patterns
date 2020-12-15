namespace FactoryMethod.PatternWithDI
{
    public class BikeRunner : TransportRunner<Bike>
    {
        private readonly Bike _bike;

        public BikeRunner(Bike bike)
        {
            _bike = bike;
        }

        protected override Bike GetTransport() => _bike;
    }
}
