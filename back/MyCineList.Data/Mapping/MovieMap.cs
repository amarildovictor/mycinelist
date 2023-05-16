using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("MOVIE");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.ImageMovie);
            
            builder.Property(x => x.IMDBID).HasMaxLength(10);
            builder.Property(x => x.IMDBAggregateRatting).HasPrecision(3,2);
            builder.Property(x => x.IMDBTitleTypeID).HasMaxLength(50);
            builder.Property(x => x.IMDBTiltleText).HasMaxLength(200);
            builder.Property(x => x.ReleaseYear);
            builder.Property(x => x.ReleaseDate);

            builder.HasIndex(x => x.IMDBID).IsUnique();
            builder.HasIndex(x => x.IMDBTiltleText);
        }
    }
}