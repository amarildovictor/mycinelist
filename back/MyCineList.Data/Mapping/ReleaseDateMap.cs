using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class ReleaseDateMap : IEntityTypeConfiguration<ReleaseDate>
    {
        public void Configure(EntityTypeBuilder<ReleaseDate> builder)
        {
            builder.ToTable("RELEASE_DATE");

            builder.HasKey(x => x.ID);

            builder.HasOne(x => x.Movie);

            builder.Property(x => x.Day);
            builder.Property(x => x.Month);
            builder.Property(x => x.Year);
        }
    }
}