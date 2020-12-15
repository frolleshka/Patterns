using Ninject;

namespace FactoryMethod.PatternWithDI
{
    class GenericTransportRunner<T> : TransportRunner<T>
        where T : ITransport
    {
        private readonly T _transport;

        public GenericTransportRunner(T kernel)
        {
            _transport = kernel;
        }

        protected override T GetTransport() => _transport;
    }
}
