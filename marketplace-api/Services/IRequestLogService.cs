using marketplace_api.Models;

namespace marketplace_api.Services;

public interface IRequestLogService
{
    public Task Log(RequestLog log);
}