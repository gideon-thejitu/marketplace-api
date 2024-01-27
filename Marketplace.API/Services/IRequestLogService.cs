using Marketplace.Domain.Entities;

namespace Marketplace.Services;

public interface IRequestLogService
{
    public Task Log(RequestLog log);
}