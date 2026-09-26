namespace Processing.Application.DTOs
{
    public sealed class ProcessingErrorResult
    {
        public Guid RawPriceId { get; }
        public string Step { get; }
        public string Message { get; }

        public ProcessingErrorResult(Guid rawPriceId, string step, string message)
        {
            RawPriceId = rawPriceId;
            Step = step;
            Message = message;
        }
    }
}
