using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Infrastructure.DbConfiguration.Write
{
    public class NoticeConfiguration : IEntityTypeConfiguration<ScrapingNotice>
    {
        public void Configure(EntityTypeBuilder<ScrapingNotice> builder)
        {
            builder.ToTable("notices");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Url).IsRequired();

            builder.Property(n => n.CreatedAt).IsRequired();

            builder.ComplexProperty(n => n.Data, b =>
            {
                b.Property(d => d.Head).HasColumnName("data_head");
                b.Property(d => d.Title).HasColumnName("data_title");
                b.Property(d => d.Meta).HasColumnName("data_meta");
            });
        }
    }
}
