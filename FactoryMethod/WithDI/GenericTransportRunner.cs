using Ninject;

namespace FactoryMethod.PatternWithDI
{
    class GenericTransportRunner<T> : TransportRunner<T>
        where T : ITransport
    {
        private readonly IKernel _kernel;

        public GenericTransportRunner(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override T GetTransport() => _kernel.Get<T>();
    }
}
