using Ninject;

namespace FactoryMethod.PatternWithDI
{
    public class BikeRunner : TransportRunner<Bike>
    {
        private readonly Bike _bike;

        public BikeRunner()
        {
            _bike = new Bike();
        }

        protected override Bike GetTransport() => _bike;
    }
}
