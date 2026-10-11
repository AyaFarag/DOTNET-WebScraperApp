using Analytics.Application.Enums;
using Analytics.Application.Interfaces.AI.Logic;

namespace Analytics.Application.Services.AI.Logic
{
    public sealed class DateRangeResolver : IDateRangeResolver
    {
        public (DateTime? From, DateTime? To) Resolve(
            DateRangeType rangeType,
            DateTime? customFrom,
            DateTime? customTo,
            DateTime now)
        {
            var today = now.Date;

            return rangeType switch
            {
                DateRangeType.None =>
                    (null, null),

                DateRangeType.Today =>
                    (today, today.AddDays(1).AddTicks(-1)),

                DateRangeType.Yesterday =>
                    (
                        today.AddDays(-1),
                        today.AddTicks(-1)
                    ),

                DateRangeType.ThisWeek => ResolveThisWeek(today),

                DateRangeType.ThisMonth =>
                    (
                        new DateTime(
                            today.Year,
                            today.Month,
                            1),
                        new DateTime(
                            today.Year,
                            today.Month,
                            1)
                        .AddMonths(1)
                        .AddTicks(-1)
                    ),

                DateRangeType.Last7Days =>
                    (
                        today.AddDays(-6),
                        today.AddDays(1).AddTicks(-1)
                    ),

                DateRangeType.Last30Days =>
                    (
                        today.AddDays(-29),
                        today.AddDays(1).AddTicks(-1)
                    ),

                DateRangeType.Custom =>
                    (customFrom, customTo),

                _ =>
                    throw new ArgumentOutOfRangeException(
                        nameof(rangeType))
            };
        }

        private static (DateTime From, DateTime To) ResolveThisWeek(DateTime today)
        {
            var diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

            var start = today.AddDays(-diff);

            var end = start.AddDays(7).AddTicks(-1);

            return (start, end);
        }
    }
}
