using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Scraper.Domain.Entities;
using Scraper.Domain.ValueObject;

namespace Scraper.Infrastructure.DbConfiguration.Write;

public class NoticeConfiguration : IEntityTypeConfiguration<ScrapingNotice>
{
    public void Configure(EntityTypeBuilder<ScrapingNotice> builder)
    {
        builder.ToTable("notices");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Url).IsRequired();

        builder.Property(n => n.ErrorScraping).IsRequired(false);

        builder.Property(n => n.CreatedAt).IsRequired();

        builder.Property(n => n.Headers)
          .HasConversion(
              metadata => JsonConvert.SerializeObject(metadata),
              json => JsonConvert.DeserializeObject<Headers>(json)
          )
          .HasColumnType("jsonb")
          .IsRequired(false);
    }
}
