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
    public class OrderConfiguration : IEntityTypeConfiguration<OrderToScrape>
    {
        public void Configure(EntityTypeBuilder<OrderToScrape> builder)
        {
            builder.ToTable("orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.CreatedAt).IsRequired();

            builder.Property(o => o.Paths);

            builder.HasMany(o => o.Notices).WithOne().IsRequired();
        }
    }
}
