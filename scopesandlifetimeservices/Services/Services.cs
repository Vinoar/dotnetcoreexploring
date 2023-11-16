namespace scopesandlifetimeservices.Services
{
    public class ScopedService : IScopedService
    {
        public Guid ScopeId { get; set; }

        public void Dispose() => GC.Collect();

        public ScopedService()
        {
            ScopeId= Guid.NewGuid();
        }
    }

    public class SingletonService : ISingletonService
    {
        public Guid SingletonId { get; set; }

        public void Dispose() => GC.Collect();
          
        public SingletonService()
        {
            SingletonId = Guid.NewGuid();
        }
    }

    public class TransientService : ITransientService
    {
        public Guid TransientId { get; set; }

        public void Dispose() => GC.Collect();

        public TransientService()
        {
            TransientId = Guid.NewGuid();
        }
    }
}
