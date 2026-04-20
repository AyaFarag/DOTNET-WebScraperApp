using System.Text.Json;
using BuildingBlocks.Observability.Abstractions.Auditing;

namespace BuildingBlocks.Observability.Auditing;

public class AuditService : IAuditService
{
    private readonly List<AuditLog> _store = new(); // replace with DB later

    public Task LogAsync(string action, string entity, object data)
    {
        _store.Add(new AuditLog
        {
            Action = action,
            Entity = entity,
            Data = JsonSerializer.Serialize(data),
            Timestamp = DateTime.UtcNow
        });

        return Task.CompletedTask;
    }
}