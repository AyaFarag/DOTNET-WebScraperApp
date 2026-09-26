using Analytics.Application.Enums;

namespace Analytics.Application.Interfaces.AI.Logic
{
    public interface IDateRangeResolver
    {
        (DateTime? From, DateTime? To) Resolve(
            DateRangeType rangeType,
            DateTime? customFrom,
            DateTime? customTo,
            DateTime now);
    }
}
