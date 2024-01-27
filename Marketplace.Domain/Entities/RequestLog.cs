namespace Marketplace.Domain.Entities;

public class RequestLog
{
    public long Id { get; set; }
    public Guid RequestLogId { get; set; }
    public int StatusCode { get; set; }
}
