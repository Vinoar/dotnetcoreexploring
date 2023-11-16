///TODO : Please don't use this style; instead, make a separate file for every interface or class.
namespace scopesandlifetimeservices.Services
{
    public interface IScopedService :IDisposable
    {
        public Guid ScopeId { get; set; }
    }

    public interface ISingletonService : IDisposable
    {
        public Guid SingletonId { get; set; }
    }

    public interface ITransientService : IDisposable
    {
        public Guid TransientId { get; set; }
    }
}
