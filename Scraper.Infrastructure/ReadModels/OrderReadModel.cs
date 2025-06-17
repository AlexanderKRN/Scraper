namespace Scraper.Infrastructure.ReadModels
{
    public class OrderReadModel
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }

        public List<NoticeReadModel> Paths { get; init; } = [];
        public List<NoticeReadModel> Notices { get; init; } = [];
    }
}
