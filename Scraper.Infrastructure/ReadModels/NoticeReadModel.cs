namespace Scraper.Infrastructure.ReadModels
{
    public class NoticeReadModel
    {
        public Guid Id { get; init; }
        public string Url { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public string Data { get; init; } = string.Empty;
    }
}
