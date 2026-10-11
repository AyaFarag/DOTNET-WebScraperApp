using Analytics.Application.Events;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;
using Analytics.Application.Interfaces.Repositories;
using System.Text.Json;

namespace Analytics.Application.EventHandler;


// Search ????
public sealed class PriceSnapshotSearchIndexHandler
{
    private readonly IPriceSnapshotRepository _repository;
    private readonly IPriceSnapshotSearchIndexer _indexer;

    public PriceSnapshotSearchIndexHandler(
        IPriceSnapshotRepository repository,
        IPriceSnapshotSearchIndexer indexer)
    {
        _repository = repository;
        _indexer = indexer;
    }

    public async Task HandleAsync(string payload,CancellationToken cancellationToken)
    {
        var message =
            JsonSerializer.Deserialize<PriceSnapshotSearchIndexRequested>(payload);

        if (message is null)
        {
            throw new InvalidOperationException(
                "Invalid PriceSnapshot search index message.");
        }

        var snapshot =  await _repository.GetByIdAsync(message.SnapshotId, cancellationToken);

        if (snapshot is null)
        {
            throw new InvalidOperationException(
                $"PriceSnapshot '{message.SnapshotId}' was not found.");
        }

        await _indexer.IndexAsync(snapshot,cancellationToken);
    }
}
