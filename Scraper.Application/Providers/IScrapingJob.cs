namespace Scraper.Application.Providers
{
    public interface IScrapingJob
    {
        Task ProcessAsync(Guid id);
    }
}