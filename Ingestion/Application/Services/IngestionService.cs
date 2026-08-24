using Application.Interfaces.Repositories;
using Ingestion.Application.DTOs;
using Ingestion.Application.Interfaces;
using Shared.Domain;
using MediatR;
using Shared.Contracts.Events.Ingestion;
using Ingestion.Domain.Entities;



namespace Ingestion.Application.Services
{
    public class IngestionService : IIngestionService
    {
        
        private IMediator _mediator ;
        private readonly IScraper _scraper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRawPriceRepository _rawPriceRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IOutboxRepository _outboxRepository;

        private readonly IIngestionExecutionRepository _executionRepository;
        public IngestionService(IScraper scraper, 
            IMediator mediator, 
            IUnitOfWork unitOfWork, 
            IRawPriceRepository rawPriceRepository,
            IIngestionExecutionRepository ingestionExecutionRepository,
            IBatchRepository batchRepository,
            IOutboxRepository outboxRepository)
        {
            _scraper = scraper;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _rawPriceRepository = rawPriceRepository;
            _executionRepository = ingestionExecutionRepository;
            _batchRepository = batchRepository;
            _outboxRepository = outboxRepository;
        }
        public async Task<List<RawPriceDto>> ScrapeAsync(string source,CancellationToken cancellationToken)
        {
          //  await using var transaction =  await _context.Database.BeginTransactionAsync(cancellationToken);
            var batch = new IngestionBatch(source);
            var execution = new IngestionExecution(batch.Id, "Carrefour");
            try
            {
                // Add Batch
                // Add Execution
                // Add RawPrices
                // Add Outbox


                // Add Batch
                await _batchRepository.AddAsync(batch, cancellationToken);

                // Add Execution
                await _executionRepository.AddAsync(execution, cancellationToken);


                batch.MarkScraping();

                var results = await _scraper.ScrapeAsync();

                var rawPrices = results
                    .Select(x => new Shared.Domain.RawPrice(
                        batch.Id,
                        x.Source,
                        x.SourceUrl,
                        x.ProductName,
                        x.RawPrice,
                        x.Currency,
                        x.CollectedAt,
                        x.RawData))
                    .ToList();

                // Add RawPrices
                await _rawPriceRepository.AddRangeAsync(rawPrices, cancellationToken);

                execution.Complete(rawPrices.Count);
                batch.Complete(rawPrices.Count);


                // Add Outbox
                var eventId = Guid.NewGuid();
                await _outboxRepository.AddAsync(eventId , @batch.Id , cancellationToken);


                await _unitOfWork.SaveChangesAsync(cancellationToken);

               // await transaction.CommitAsync(cancellationToken);

                return results;
            }
            catch (Exception ex)
            {
                execution.Fail(ex.Message);

                batch.Fail(ex.Message);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

             //   await transaction.RollbackAsync(cancellationToken);

                throw;
            }

         

        }

        #region scrap test 1
        //public async Task<List<RawPriceDto>> ScrapeAsync(string source,CancellationToken cancellationToken)
        //{
        //    var batch = new IngestionBatch(source);
        //    var eventId = Guid.NewGuid();

        //    await _batchRepository.AddAsync(batch,cancellationToken);

        //    try
        //    {
        //        batch.MarkScraping();

        //        var results = await _scraper.ScrapeAsync(source,cancellationToken);

        //        var rawPrices = results
        //            .Select(x => new RawPrice(
        //                batch.Id,
        //                x.Source,
        //                x.SourceUrl,
        //                x.ProductName,
        //                x.RawPrice,
        //                x.Currency,
        //                x.CollectedAt,
        //                x.RawData))
        //            .ToList();

        //        await _rawPriceRepository.AddRangeAsync(rawPrices,cancellationToken);

        //        batch.Complete(rawPrices.Count);

        //        var @event = new PriceDataCollectedEvent(EventId,batch.Id);
        //        var payload = JsonSerializer.Serialize(@event);

        //        await _outboxService.AddAsync(@event,cancellationToken);

        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        batch.Fail(ex.Message);

        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        throw;
        //    }
        //}
        #endregion

        #region scrap normal without batch and execution and outbox
        public async Task<List<RawPriceDto>> RunScrapingAsync(CancellationToken cancellationToken)
        {

            var batchId = Guid.NewGuid();

            var execution = new IngestionExecution(
                batchId,
                "Carrefour");

            await _executionRepository.AddAsync(
                execution,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            var data = await _scraper.ScrapeAsync();


            // 3. Convert DTOs → Domain entities
            var rawPrices = data
                .Select(x => new Shared.Domain.RawPrice(
                    batchId,
                    x.Source,
                    x.SourceUrl,
                    x.ProductName,
                    x.RawPrice,
                    x.Currency,
                    x.CollectedAt,
                    x.RawData))
                .ToList();

            // 4. Add to repository
            await _rawPriceRepository.AddRangeAsync(
               rawPrices,
               cancellationToken);

            // 5. Commit to database
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);


            foreach (var item in data)
            {
                await _mediator.Publish(new PriceScrapedEvent
                {
                    BatchId = batchId,
                });
            }

            return data;
        }
        #endregion

        public async Task<HashSet<string>> RunEmailScrapingAsync()
        {
     
            var data = await _scraper.EmailScrapeAsync();

            foreach (var item in data)
            {
                await _mediator.Publish(new EmailScrapedEvent
                {
                     Email = item

                });
            }

            return data;
        }
    }
}
