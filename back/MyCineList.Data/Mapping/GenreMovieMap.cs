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

            builder.HasKey(x => new { x.GenresIMDBGenreID, x.MovieID });

            builder.HasOne(x => x.Movie).WithMany(m => m.GenresMovie).HasForeignKey(x => x.MovieID);
            builder.HasOne(x => x.Genre).WithMany(g => g.GenreMovie).HasForeignKey(x => x.GenresIMDBGenreID);
        }
    }
}