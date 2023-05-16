using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class GenreMovieMap : IEntityTypeConfiguration<GenreMovie>
    {
        public void Configure(EntityTypeBuilder<GenreMovie> builder)
        {
            builder.ToTable("GENRE_MOVIE");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Movie);

            builder.Property(x => x.IMDBGenreID).HasMaxLength(50);

            builder.HasIndex(x => x.IMDBGenreID);
        }
    }
}