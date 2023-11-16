namespace scopesandlifetimeservices.Services
{
    public class SampleDIConsumer : ISample
    {
        readonly ISingletonService _singletonService;
        readonly IScopedService _scopedService;
        readonly ITransientService _transientService;

        public SampleDIConsumer(
            ISingletonService singletonService, 
            IScopedService scopedService, 
            ITransientService transientService)
        {
            _singletonService = singletonService;
            _scopedService = scopedService;
            _transientService = transientService;
        } 

        public Guid GetScopedID()
        {
            return _scopedService.ScopeId;
        }

        public Guid GetSingletonID()
        {
            return _singletonService.SingletonId;
        }

        public Guid GetTransientID()
        {
            return _transientService.TransientId;
        }
    }
}
