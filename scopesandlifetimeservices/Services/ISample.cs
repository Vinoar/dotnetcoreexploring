namespace scopesandlifetimeservices.Services
{
    public interface ISample
    {
        Guid GetSingletonID();
        Guid GetScopedID();
        Guid GetTransientID();
    }
}
