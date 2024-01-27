using Marketplace.Domain.Data;
using Marketplace.Domain.Entities;
using Marketplace.Infrastructure.Data;

namespace Marketplace.Services;

public class RequestLogService : IRequestLogService
{
    private readonly DataContext _context;

    public RequestLogService(DataContext context)
    {
        _context = context;
    }

    public async Task Log(RequestLog log)
    {
        _context.RequestLogs.Add(log);

        await _context.SaveChangesAsync();
    }
}
