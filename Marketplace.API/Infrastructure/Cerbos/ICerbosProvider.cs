using Cerbos.Sdk;

namespace Marketplace.Infrastructure.Cerbos;

public interface ICerbosProvider
{
    CerbosClient Client();
}