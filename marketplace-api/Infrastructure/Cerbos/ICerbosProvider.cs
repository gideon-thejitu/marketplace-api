using Cerbos.Sdk;

namespace marketplace_api.Infrastructure.Cerbos;

public interface ICerbosProvider
{
    CerbosClient Client();
}