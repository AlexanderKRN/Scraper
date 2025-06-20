using Hangfire;

namespace Scraper.Infrastructure.Jobs;

/// <summary>
/// Обработчик задач по расписанию библиотеки Hangfire
/// </summary>
public class HangfireWorker
{
    /// <summary>
    /// Запуск повторяющихся задач
    /// </summary>
    public static void StartRecurringJobs()
    {
        var orderId = Guid.Parse(Common.ScrapinConstants.ORDER_ID);

        RecurringJob.AddOrUpdate<IScrapingJob>(
            "collect data",
            job => job.ProcessAsync(orderId),
            "*/3 * * * *");
    }
}
