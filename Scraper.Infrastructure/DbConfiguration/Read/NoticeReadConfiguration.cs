using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.DbConfiguration.Read;

/// <summary>
/// Конфигурация чтения отчета
/// </summary>
public class NoticeReadConfiguration : IEntityTypeConfiguration<ScrapingNotice>
{
    public void Configure(EntityTypeBuilder<ScrapingNotice> builder)
    {
        builder.ToTable("notices");

        builder.HasKey(n => n.Id);

        builder.OwnsOne(n => n.Headers, h =>
        {
            h.ToJson();
            h.OwnsMany(h => h.Meta, m =>
            {
                m.OwnsMany(m => m.Attributes);
            });
        });
    }
}
