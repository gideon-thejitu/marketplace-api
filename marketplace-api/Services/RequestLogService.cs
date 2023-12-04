using marketplace_api.Data;
using marketplace_api.Models;

namespace marketplace_api.Services;

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
